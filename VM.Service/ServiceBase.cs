using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Service
{
    public abstract class ServiceBase : IDisposable
    {
        public IList<IDisposable> DisposableObjects { get; private set; }
        public ServiceBase()
        {
            this.DisposableObjects = new List<IDisposable>();
        }

        protected void AddDisposableObject(object obj)
        {
            IDisposable disp = obj as IDisposable;
            if (null != disp)
            {
                this.DisposableObjects.Add(disp);
            }
        }

        public void Dispose()
        {
            foreach (var obj in this.DisposableObjects)
            {
                if (null != obj)
                {
                    obj.Dispose();
                }
            }
        }
    }
}
