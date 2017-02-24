﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data;

namespace DataAccessLayer
{
    public class CharityAccessor : IDataAccessor
    {
        public Charity CharityInstance { get; set; }
        public List<Charity> CharityList { get; private set; }

        public string CreateScript
        {
            get
            {
                return @"sp_create_charity";
            }
        }

        public string DeactivateScript
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string RetrieveListScript
        {
            get
            {
                return @"sp_retrieve_charity_list";
            }
        }

        public string RetrieveSearchScript
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string RetrieveSingleScript
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string UpdateScript
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void ReadList(SqlDataReader reader)
        {
            CharityList = new List<Charity>();
            while (reader.Read()) {
                CharityList.Add(new Charity
                {
                    CharityID = reader.GetInt32(0),
                    UserID = reader.GetInt32(1),
                    EmployeeID = reader.GetInt32(2),
                    CharityName = reader.GetString(3),
                    ContactFirstName = reader.GetString(4),
                    ContactLastName = reader.GetString(5),
                    PhoneNumber = reader.GetString(6),
                    Email = reader.GetString(7),
                    ContactHours = reader.GetString(8)
                });
            }
        }

        public void ReadSingle(SqlDataReader reader)
        {
            CharityInstance = new Charity
            {
                CharityID = reader.GetInt32(0),
                UserID = reader.GetInt32(1),
                CharityName = reader.GetString(2),
                ContactFirstName = reader.GetString(3),
                ContactLastName = reader.GetString(4),
                PhoneNumber = reader.GetString(5),
                Email = reader.GetString(6),
                ContactHours = reader.GetString(7)
            };
        }

        public void SetCreateParameters(SqlCommand cmd)
        {
            cmd.Parameters.Add("@USER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@EMPLOYEE_ID", SqlDbType.Int);
            cmd.Parameters.Add("@CHARITY_NAME", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@CONTACT_FIRST_NAME", SqlDbType.NVarChar, 150);
            cmd.Parameters.Add("@CONTACT_LAST_NAME", SqlDbType.NVarChar, 150);
            cmd.Parameters.Add("@PHONE_NUMBER", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@CONTACT_HOURS", SqlDbType.NVarChar, 150);
            cmd.Parameters["@USER_ID"].Value = CharityInstance.UserID;
            cmd.Parameters["@EMPLOYEE_ID"].Value = CharityInstance.EmployeeID;
            cmd.Parameters["@CHARITY_NAME"].Value = CharityInstance.CharityName;
            cmd.Parameters["@CONTACT_FIRST_NAME"].Value = CharityInstance.ContactFirstName;
            cmd.Parameters["@CONTACT_LAST_NAME"].Value = CharityInstance.ContactLastName;
            cmd.Parameters["@PHONE_NUMBER"].Value = CharityInstance.PhoneNumber;
            cmd.Parameters["@EMAIL"].Value = CharityInstance.Email;
            cmd.Parameters["@CONTACT_HOURS"].Value = CharityInstance.ContactHours;
        }

        public void SetKeyParameters(SqlCommand cmd)
        {
            throw new NotImplementedException();
        }

        public void SetRetrieveSearchParameters(SqlCommand cmd)
        {
            throw new NotImplementedException();
        }

        public void SetUpdateParameters(SqlCommand cmd)
        {
            throw new NotImplementedException();
        }
    }
}