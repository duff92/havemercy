using UnityEngine;
using System.Collections;

public class CustomParticle : MonoBehaviour
{
	/// <summary>
	/// Particle data structure used by the shader and the compute shader.
	/// </summary>
	private struct Particle
	{
		public Vector3 position;
		public Vector3 velocity;
	}

	/// <summary>
	/// Size in octet of the Particle struct.
	/// </summary>
	private const int SIZE_PARTICLE = 24;

	/// <summary>
	/// Number of Particle created in the system.
	/// </summary>
	public int particleCount = 200;

	/// <summary>
	/// Material used to draw the Particle on screen.
	/// </summary>
	public Material material;

	/// <summary>
	/// Compute shader used to update the Particles.
	/// </summary>
	public ComputeShader computeShader;

	/// <summary>
	/// Id of the kernel used.
	/// </summary>
	private int mComputeShaderKernelID;

	/// <summary>
	/// Buffer holding the Particles.
	/// </summary>
	ComputeBuffer particleBuffer;

	/// <summary>
	/// Number of particle per warp.
	/// </summary>
	private const int WARP_SIZE = 8;

	/// <summary>
	/// Number of warp needed.
	/// </summary>
	private int mWarpCount;
    

	void Start()
	{
		// Calculate the number of warp needed to handle all the particles
		if (particleCount <= 0)
			particleCount = 1;
		mWarpCount = Mathf.CeilToInt((float)particleCount / WARP_SIZE);

		// Initialize the Particle at the start
		Particle[] particleArray = new Particle[particleCount];
		for (int i = 0; i < particleCount; ++i)
		{
			particleArray[i].position.x = this.transform.position.x + Random.value;
			particleArray[i].position.y = this.transform.position.y + Random.value;
			particleArray[i].position.z = this.transform.position.z + Random.value;

            particleArray[i].velocity.x = 0;
			particleArray[i].velocity.y = 0;
			particleArray[i].velocity.z = 0;
		}

		// Create the ComputeBuffer holding the Particles
		particleBuffer = new ComputeBuffer(particleCount, SIZE_PARTICLE);
		particleBuffer.SetData(particleArray);

		// Find the id of the kernel
		mComputeShaderKernelID = computeShader.FindKernel("CSMain");
        
		// Bind the ComputeBuffer to the shader and the compute shader
		computeShader.SetBuffer(mComputeShaderKernelID, "particleBuffer", particleBuffer);
		material.SetBuffer("particleBuffer", particleBuffer);
	}

	void OnDestroy()
	{
		if (particleBuffer != null)
			particleBuffer.Release();
	}
	
	// Update is called once per frame
	void Update()
	{
        float[] objectPos = { this.transform.position.x, this.transform.position.y, this.transform.position.z };
		// Send datas to the compute shader
		computeShader.SetFloat("deltaTime", Time.deltaTime);
        computeShader.SetFloats("objectPos", objectPos);
        // Update the Particles
		computeShader.Dispatch(mComputeShaderKernelID, mWarpCount, 1, 1);
	}

	void OnRenderObject()
	{
		material.SetPass(0);
		Graphics.DrawProcedural(MeshTopology.Points, 1, particleCount);
	}
}
