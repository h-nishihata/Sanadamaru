using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

//namespace TouchFubuki {

	public class UvService : MonoBehaviour {
		public int Capasity { get; private set; }
		public Vector2[] Uvs { get; private set; }
		public ComputeBuffer UvBuf { get; private set; }

		public UvService(int capasity) {
			this.Capasity = capasity;
			this.Uvs = new Vector2[capasity];
			this.UvBuf = new ComputeBuffer(capasity, Marshal.SizeOf(Uvs[0]));
		}

		public Vector2 this[int index] {
			get { return Uvs[index]; }
			set { Uvs[index] = value; }
		}
		public void Save() {
			UvBuf.SetData(Uvs);
		}

		#region IDisposable implementation
		private bool _disposed = false;
		public void Dispose () {
			if (_disposed)
				return;
			_disposed = true;

			UvBuf.Release();
		}

		#endregion


	}
//}