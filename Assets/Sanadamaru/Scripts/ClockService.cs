using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

//namespace TouchFubuki {

	public class ClockService : MonoBehaviour {
		public int Capasity { get; private set; }
		public Clock[] Clocks { get; private set; }
		public ComputeBuffer CurrBuf { get; private set; }
		public ComputeBuffer NextBuf { get; private set; }

		public ClockService(int capasity) {
			Capasity = capasity;
			Clocks = new Clock[capasity];
			for (var i = 0; i < Clocks.Length; i++)
				Clocks[i] = Clock.Null;

			CurrBuf = new ComputeBuffer(capasity, Marshal.SizeOf(typeof(Clock)));
			NextBuf = new ComputeBuffer(capasity, Marshal.SizeOf(typeof(Clock)));
			CurrBuf.SetData(Clocks);
			NextBuf.SetData(Clocks);
		}

		public Clock this[int index] {
			get { return Clocks[index]; }
			set { Clocks[index] = value; }
		}
		public void Swap() {
			var tmpBuf = CurrBuf; CurrBuf = NextBuf; NextBuf = tmpBuf;
		}
		public void Load() {
			CurrBuf.GetData(Clocks);
		}
		public void Save() {
			CurrBuf.SetData(Clocks);
		}

		#region IDisposable implementation
		private bool _disposed = false;
		public void Dispose () {
			if (_disposed)
				return;
			_disposed = true;

			CurrBuf.Release();
			NextBuf.Release();
		}
		#endregion
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Clock {
		public float t;
		public int stencil;

		public Clock(float t, int stencil) {
			this.t = t;
			this.stencil = stencil;
		}

		public static readonly Clock Null = new Clock(-1f, 0);
	}
//}