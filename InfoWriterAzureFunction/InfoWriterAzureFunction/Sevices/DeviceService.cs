using InfoWriterAzureFunction.Database.Context;
using InfoWriterAzureFunction.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoWriterAzureFunction.Sevices
{
    public class DeviceService
    {
        public PublicContext context;
        public DeviceService(PublicContext pc)
        {
            context = pc;
        }

        public async Task<Device> AddDevice(DeviceInfo deviceInfo)
        {
            var device = new Device
            {
                ComputerName = deviceInfo.compname,
                DotNewVersion = deviceInfo.dotnetversion,
                Osname = deviceInfo.osname,
                TimeZone = deviceInfo.timezone
            };
            device.StatusStorage.Add(new StatusStorage
            {
                StatusId = deviceInfo.statusid,
                ChangeStatusTime = DateTime.Now,
            });
            await context.Device.AddAsync(device);
            await context.SaveChangesAsync();
            return device;
        }

        public async Task<Device> GetDevice(string compName, string oSName)
        {
            var device = await context.Device.FirstOrDefaultAsync(x => x.ComputerName == compName && x.Osname == oSName);
            return device;
        }

        public async Task<Device> UpdateDevice(int id, DeviceInfo deviceInfo)
        {
            var device = await context.Device.FirstOrDefaultAsync(x => x.Id == id);
            device.DotNewVersion = deviceInfo.dotnetversion;
            device.TimeZone = deviceInfo.timezone;
            var lastStatusStorage = await context.StatusStorage.Where(x => x.DeviceId == device.Id).OrderByDescending(x => x.ChangeStatusTime).FirstOrDefaultAsync();
            var status = await context.Status.FirstOrDefaultAsync(x => x.Id == deviceInfo.statusid);
            if (status == null) throw new Exception($"status with id {deviceInfo.statusid} does not exist");
            if(lastStatusStorage.StatusId != deviceInfo.statusid)
            {
                device.StatusStorage.Add(new StatusStorage
                {
                    StatusId = deviceInfo.statusid,
                    ChangeStatusTime = DateTime.Now,
                });
            }
            await context.SaveChangesAsync();
            return device;
        }

        public async Task<List<StatusStorage>> GetOnlineHystoryDevice(string compname, string osname)
        {
            var hystory = await context.StatusStorage.Include(x => x.Device).Include(x => x.Status).Where(x => x.Device.ComputerName == compname && x.Device.Osname == osname).Take(50).ToListAsync();
            return hystory;
        }
    }
}
