using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MicrosoftAzureRedisCache
{

    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            InitCache();
        }

        IDatabase cache;

        private void InitCache()
        {
            cache = CacheManager.Connection.GetDatabase();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cache.StringSet("key1", "valor", TimeSpan.FromSeconds(5));
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            string cacheResult = cache.StringGet("key1");

            if (string.IsNullOrWhiteSpace(cacheResult))
            {
                MessageBox.Show("LLave Expirada");
                return;
            }

            lstResult.Items.Add(cacheResult);
        }

        private void btnSendObjects_Click(object sender, EventArgs e)
        {
            IEnumerable<Employee> Employees = new[] {
                new Employee { Id = 1, Name = "PEPE" },
                new Employee { Id = 1, Name = "PEPE" },
                new Employee { Id = 1, Name = "PEPE" },
                new Employee { Id = 1, Name = "PEPE" },
                new Employee { Id = 1, Name = "PEPE" },
            };

            cache.StringSet("employeeList", JsonConvert.SerializeObject(Employees)
                , TimeSpan.FromSeconds(10));
        }

        private void btnGetObjects_Click(object sender, EventArgs e)
        {
            var _cacheResult = cache.StringGet("employeeList");
            if (string.IsNullOrWhiteSpace(_cacheResult))
            {
                MessageBox.Show("LLave Expirada");
                return;
            }

            dataGridView1.DataSource = 
                (IList<Employee>)JsonConvert.DeserializeObject<IEnumerable<Employee>>(_cacheResult);
        }
    }
}
