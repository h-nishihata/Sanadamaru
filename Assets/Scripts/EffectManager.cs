using UnityEngine;
using System.Collections;
using nobnak.Sampling;
using nobnak.Timer;
using System.Runtime.InteropServices;

//namespace TouchFubuki {

	public class EffectManager : MonoBehaviour {
		public const int NUM_THREADS_IN_GROUP = 64;

		public int numGroups = 40;
		public int numTouches = 1;

		private ClockService _clocks;
		private UvService _uvs;
		private ComputeBuffer _rotations;

		void OnDestroy() {
			
			_clocks.Dispose();
			_uvs.Dispose();
			_rotations.Release();

		}


		void Start() {
			
			var numThreads = numGroups * NUM_THREADS_IN_GROUP;

			_clocks = new ClockService(numThreads);
			_uvs = new UvService(numThreads);
			_rotations = new ComputeBuffer(numThreads, 16 * Marshal.SizeOf(typeof(float)));

		}


		void Update() {

			var time = TimeUtil.ShaderTime2014;
			Shader.SetGlobalVector("_DateTime", time);
			Shader.SetGlobalBuffer("ClockBufCurr", _clocks.CurrBuf);
			Shader.SetGlobalBuffer("RotationBuf", _rotations);
			Shader.SetGlobalBuffer("UvBuf", _uvs.UvBuf);

		}

	}
//}