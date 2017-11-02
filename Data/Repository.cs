using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace BB

{
    public class Repository
    {

        private string server = "Server =192.168.100.230;";
        private string database = "database = plc;";        
        private string uid = "uid=ABCD;";                  
        private string password = "pwd = ABC123;";          
        private string charset = "Charset=utf8;";

        public enum SQLActions { update, replace };

 

        /// <summary>
        /// Retrieves data of objects type T from SQL.</summary>
        /// <param name="groupName"> Enter name for filter, table must have field: name</param>
        /// <returns>
        /// Returns List of type T.</returns>
        public List<T> RetrieveObjects<T>(string groupName = "") where T : new()
        {
            var list = new List<T>();
            using (MySqlConnection conn = new MySqlConnection())
            {
                conn.ConnectionString = password + uid + database + server + charset;
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                        cmd.CommandText =
               $"SELECT * FROM {typeof(T).Name.ToString()}";
                    if (groupName != "")
                    {
                        cmd.CommandText += " WHERE name = @groupname";

                        cmd.Parameters.AddWithValue("@groupname", groupName);
                    }

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            switch (typeof(T).Name)
                            {
                                case "Workers":
                                    var worker = (T)Activator.CreateInstance(typeof(T), new object[]
                                    { reader.GetInt32("id"),
                                      reader.GetString("name"),
                                      reader.GetString("lastname"),
                                      reader.GetString("rfid")}
                                    );
                                    list.Add(worker);
                                    break;
                                case "Products":
                                    var product = (T)Activator.CreateInstance(typeof(T), new object[] {
                                      reader.GetInt32("id"),
                                      reader.GetString("name"),
                                      reader.GetInt32("fillet size"),
                                      reader.GetFloat("fillet qty"),
                                      reader.GetFloat("liquid qty"),
                                      reader.GetString("container name"),
                                      reader.GetString("additives") }
                                    );
                                    list.Add(product);
                                    break;
                                case "Workplaces":
                                    var workplace = (T)Activator.CreateInstance(typeof(T), new object[] {
                                      reader.GetInt32("placeid"),
                                      reader.GetInt32("workerid"),
                                      reader.GetInt32("occupied"),
                                      reader.GetString("name")
                                    }
                                    );
                                    list.Add(workplace);
                                    break;
                                default:
                                    return null;
                            }
                        }
                    }
                }
                return list;
            }
        }
        



        /// <summary>
        /// Updates objects type T in SQL.</summary>
        /// <param name="obj"> object to update</param>
        /// <returns>
        /// Returns int: -2 cast error, -3 object not implemented in method else check sql doc ExecuteNonQuery().</returns>
        public int WriteObjects<T>(object obj,SQLActions action)
        {
            using (MySqlConnection conn = new MySqlConnection())
            {


                conn.ConnectionString = password + uid + database + server + charset;
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    switch (typeof(T).Name)
                    {
                        case "Workers":
                            var worker = obj as Workers;
                            if (worker != null)
                            {
                                if (action == SQLActions.replace)
                                {
                                    cmd.CommandText =
                                     $"REPLACE INTO workers VALUES (@id, @name, @lastname,@rfid)";
                                }
                                else if (action == SQLActions.update)
                                {
                                    cmd.CommandText = $"UPDATE workers SET id=@id, name=@name, lastname=@lastname,rfid=@rfid)";
                                }
                                cmd.Parameters.AddWithValue("@id", worker.ID);
                                cmd.Parameters.AddWithValue("@name", worker.FirstName);
                                cmd.Parameters.AddWithValue("@lastname", worker.LastName);
                                cmd.Parameters.AddWithValue("@rfid", worker.RFID);
                                break;
                            }
                            else
                                return -2;

                        case "Products":
                            var product = obj as Products;
                            if (product != null)
                            {
                                if (action == SQLActions.replace)
                                {
                                    cmd.CommandText =
                               $"REPLACE INTO products VALUES (@id, @name, @filletsize,@filletqty,@liquidqty,@containername,@additives)";
                                }
                                else if (action == SQLActions.update)
                                {
                                    cmd.CommandText = "UPDATE products SET name=@name," +
                                        "`fillet size`=@filletsize, `fillet qty`=@filletqty, `liquid qty`=@liquidqty, " +
                                        "`container name`=@containername, `additives`=@additives where id=@id ";
                                }
                                cmd.Parameters.AddWithValue("@id", product.ID);
                                cmd.Parameters.AddWithValue("@name", product.Name);
                                cmd.Parameters.AddWithValue("@filletsize", product.FilletSize);
                                cmd.Parameters.AddWithValue("@filletqty", product.FilletQty);
                                cmd.Parameters.AddWithValue("@liquidqty", product.LiquidQty);
                                cmd.Parameters.AddWithValue("@containername", product.ContainerName);
                                cmd.Parameters.AddWithValue("@additives", product.Additives);
                                break;
                            }
                            else
                                return -2;



                        case "Workplaces":
                            var workplace = obj as Workplaces;
                            if (workplace != null)
                            {
                                if (action == SQLActions.replace)
                                {
                                    cmd.CommandText = 
                               $"REPLACE INTO workplaces  (`placeid`, `workerid`, `occupied`, `name`) VALUES (@placeid, @workerid,@occupied,@groupName)";
                                }
                                else if (action == SQLActions.update)
                                {
                                    cmd.CommandText = "UPDATE workplaces SET workerid=@workerid, occupied=@occupied where placeid=@placeid and name=@groupName";
                                }
                                cmd.Parameters.AddWithValue("@placeid", workplace.PlaceID);
                                cmd.Parameters.AddWithValue("@groupName", workplace.GroupName);
                                cmd.Parameters.AddWithValue("@workerid", workplace.WorkerID);
                                cmd.Parameters.AddWithValue("@occupied", workplace.Occupied);
                              

                                break;
                            }
                            else
                                return -2;


                        default:
                            return -3;
                    }

                        return cmd.ExecuteNonQuery();
                }
            }
        }





    }
}