using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VM_MVC
{
    public abstract class VmController : Controller
    {
        public IList<IDisposable> DisposableObjects { get; private set; }
        public VmController()
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var item in DisposableObjects)
                {
                    if (null != item)
                    {
                        item.Dispose();
                    }
                }
            }
            base.Dispose(disposing);
        }
    }
}