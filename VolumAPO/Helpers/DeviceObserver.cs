using AudioSwitcher.AudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumAPO.Helpers
{
    public class DeviceObserver : IObserver<DeviceChangedArgs>
    {
        private RightClickMenuHelper rightClickMenuHelper;

        public DeviceObserver(RightClickMenuHelper rightClickMenuHelper)
        {
            this.rightClickMenuHelper = rightClickMenuHelper;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public async void OnNext(DeviceChangedArgs value)
        {
            if (value.Device.IsDefaultDevice)
            {
                await rightClickMenuHelper.UpdateDefaultDeviceByGuid(value.Device.Id, value.Device.DeviceType);
                try
                {
                    GlobalHelpers.CurrentDeviceChannels = value.Device.ChannelCount;
                }
                catch (Exception ex) { }
            }
        }
    }
}
