﻿using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class VehicleManager : IVehicleManager
    {
        /// <summary>
        /// Victor Algarin
        /// Created 2017/03/01
        /// 
        /// Retrieves the details for a specific vehicle through an id
        /// </summary>
        public Vehicle RetreiveVehicleById(int vehicleId)
        {
            Vehicle vehicle = null;

            try
            {
                vehicle = VehicleAccessor.RetreiveVehicleByVehicleId(vehicleId);
            }
            catch (Exception)
            {

                throw new ApplicationException("There was a problem retreiving the requested vehicle from the database");
            }

            return vehicle;
        }

        /// <summary>
        /// Mason Allen
        /// Created 03/01/2017
        /// Creates a new vehicle record
        /// </summary>
        /// <param name="newVehicle"></param>
        /// <returns>An int of 1 for success, 0 for fail</returns>
        public int CreateVehicle(Vehicle newVehicle)
        {
            int success;
            try
            {
                success = VehicleAccessor.CreateVehicle(newVehicle);
            }
            catch (Exception)
            {
                throw new ApplicationException("There was a problem saving the requested vehicle");
            }
            return success;
        }
    }
}