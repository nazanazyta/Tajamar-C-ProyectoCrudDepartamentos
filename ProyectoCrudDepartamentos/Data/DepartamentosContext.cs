using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ProyectoCrudDepartamentos.Models;

namespace ProyectoCrudDepartamentos.Data
{
    public class DepartamentosContext
    {
        SqlDataAdapter ad;
        DataTable tabla;
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public DepartamentosContext()
        {
            String cadena = "Data Source=localhost;Initial Catalog=HOSPITAL;User ID=sa;Password=MCSD2020";
            this.ad = new SqlDataAdapter("select * from dept", cadena);
            this.tabla = new DataTable();
            this.ad.Fill(this.tabla);
            this.cn = new SqlConnection(cadena);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from datos in this.tabla.AsEnumerable()
                           select datos;
            List<Departamento> departamentos = new List<Departamento>();
            foreach (var row in consulta)
            {
                Departamento dep = new Departamento();
                dep.IdDepartamento = row.Field<int>("dept_no");
                dep.Nombre = row.Field<String>("dnombre");
                dep.Localidad = row.Field<String>("loc");
                departamentos.Add(dep);
            }
            return departamentos;
        }

        public Departamento GetDepartamento(int iddept)
        {
            var consulta = from datos in this.tabla.AsEnumerable()
                           where datos.Field<int>("dept_no") == iddept
                           select datos;
            var row = consulta.First();
            Departamento dept = new Departamento();
            dept.IdDepartamento = row.Field<int>("dept_no");
            dept.Nombre = row.Field<String>("dnombre");
            dept.Localidad = row.Field<String>("loc");
            return dept;
        }

        public List<Departamento> GetDepartamentoNombre(String nombre)
        {
            var consulta = from datos in this.tabla.AsEnumerable()
                           //where datos.Field<String>("dnombre").ToLower() == nombre.ToLower()
                           where datos.Field<String>("dnombre").ToLower().Contains(nombre.ToLower())
                           select datos;
            List<Departamento> departamentos = new List<Departamento>();
            foreach (var row in consulta)
            {
                Departamento dep = new Departamento();
                dep.IdDepartamento = row.Field<int>("dept_no");
                dep.Nombre = row.Field<String>("dnombre");
                dep.Localidad = row.Field<String>("loc");
                departamentos.Add(dep);
            }
            return departamentos;
        }

        public void CreateDepartamento(int iddept, String nombre, String localidad)
        {
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = "insert into dept values(@iddept, @nombre, @loc)";
            this.com.Parameters.AddWithValue("@iddept", iddept);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@loc", localidad);
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void UpdateDepartamento(int iddept, String nombre, String localidad)
        {
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = "update dept set dnombre=@nombre, loc=@loc where dept_no=@iddept";
            this.com.Parameters.AddWithValue("@iddept", iddept);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@loc", localidad);
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void DeleteDepartamento(int iddept)
        {
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = "delete from dept where dept_no=@iddept";
            this.com.Parameters.AddWithValue("@iddept", iddept);
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
