using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework7
{
    public partial class Form1 : Form
    {
        // Словарь с ценами на топливо
        private Dictionary<string, decimal> fuelPrices = new Dictionary<string, decimal>
        {
            {"АИ-80", 150.5m},
            {"АИ-92", 165.8m},
            {"АИ-95", 178.2m},
            {"АИ-98", 195.7m},
            {"Дизель", 142.3m},
            {"Газ", 85.4m}
        };

        // Словарь с ценами на товары в магазине
        private Dictionary<string, decimal> shopPrices = new Dictionary<string, decimal>
        {
            {"ХотДог", 450.0m},
            {"Гамбургер", 650.0m},
            {"Фри", 350.0m},
            {"Кола", 250.0m}
        };

        public Form1()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
                UpdatePriceFromComboBox();
            }

            radioButton1.Checked = true;
            UpdateTextBoxStates();

            InitializeShop();

            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            radioButton2.CheckedChanged += RadioButton2_CheckedChanged;
            quantity.TextChanged += Quantity_TextChanged;
            summa.TextChanged += Summa_TextChanged;
            price.TextChanged += Price_TextChanged;

            checkBox1.CheckedChanged += CheckBox1_CheckedChanged;
            checkBox2.CheckedChanged += CheckBox2_CheckedChanged;
            checkBox3.CheckedChanged += CheckBox3_CheckedChanged;
            checkBox4.CheckedChanged += CheckBox4_CheckedChanged;

            textBox8.TextChanged += ShopQuantity_TextChanged;
            textBox9.TextChanged += ShopQuantity_TextChanged;
            textBox10.TextChanged += ShopQuantity_TextChanged;
            textBox11.TextChanged += ShopQuantity_TextChanged;

            textBox4.TextChanged += ShopPrice_TextChanged;
            textBox5.TextChanged += ShopPrice_TextChanged;
            textBox6.TextChanged += ShopPrice_TextChanged;
            textBox7.TextChanged += ShopPrice_TextChanged;

            button1.Click += Button1_Click;
        }

        private void InitializeShop()
        {
            textBox4.Enabled = false;
            textBox8.Enabled = false;
            textBox5.Enabled = false;
            textBox9.Enabled = false;
            textBox6.Enabled = false;
            textBox10.Enabled = false;
            textBox7.Enabled = false;
            textBox11.Enabled = false;

            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
        }



        #region Автозаправка

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePriceFromComboBox();
            CalculateTotal();
        }

        private void UpdatePriceFromComboBox()
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedFuel = comboBox1.SelectedItem.ToString();
                if (fuelPrices.ContainsKey(selectedFuel))
                {
                    price.Text = fuelPrices[selectedFuel].ToString("F2");
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTextBoxStates();
            CalculateTotal();
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTextBoxStates();
            CalculateTotal();
        }

        private void UpdateTextBoxStates()
        {
            if (radioButton1.Checked)
            {
                quantity.Enabled = true;
                summa.Enabled = false;
                summa.Text = "";
            }
            else if (radioButton2.Checked)
            {
                quantity.Enabled = false;
                summa.Enabled = true;
                quantity.Text = "";
            }
        }

        private void Quantity_TextChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                CalculateTotal();
            }
        }

        private void Summa_TextChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                CalculateTotal();
            }
        }

        private void Price_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            decimal priceValue = 0;
            decimal totalCost = 0;

            if (decimal.TryParse(price.Text, out priceValue))
            {
                if (radioButton1.Checked && !string.IsNullOrEmpty(quantity.Text))
                {
                    decimal quantityValue = 0;
                    if (decimal.TryParse(quantity.Text, out quantityValue))
                    {
                        totalCost = priceValue * quantityValue;
                    }
                }
                else if (radioButton2.Checked && !string.IsNullOrEmpty(summa.Text))
                {
                    decimal summaValue = 0;
                    if (decimal.TryParse(summa.Text, out summaValue))
                    {
                        totalCost = summaValue;
                    }
                }
            }

            label11.Text = totalCost.ToString("F2");
        }

        #endregion



        #region Магазин

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox4.Enabled = true;
                textBox8.Enabled = true;
                textBox4.Text = shopPrices["ХотДог"].ToString("F2");
                textBox8.Text = "1";
            }
            else
            {
                textBox4.Enabled = false;
                textBox8.Enabled = false;
                textBox4.Text = "";
                textBox8.Text = "";
            }
            CalculateShopTotal();
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox5.Enabled = true;
                textBox9.Enabled = true;
                textBox5.Text = shopPrices["Гамбургер"].ToString("F2");
                textBox9.Text = "1";
            }
            else
            {
                textBox5.Enabled = false;
                textBox9.Enabled = false;
                textBox5.Text = "";
                textBox9.Text = "";
            }
            CalculateShopTotal();
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                textBox6.Enabled = true;
                textBox10.Enabled = true;
                textBox6.Text = shopPrices["Фри"].ToString("F2");
                textBox10.Text = "1";
            }
            else
            {
                textBox6.Enabled = false;
                textBox10.Enabled = false;
                textBox6.Text = "";
                textBox10.Text = "";
            }
            CalculateShopTotal();
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                textBox7.Enabled = true;
                textBox11.Enabled = true;
                textBox7.Text = shopPrices["Кола"].ToString("F2");
                textBox11.Text = "1";
            }
            else
            {
                textBox7.Enabled = false;
                textBox11.Enabled = false;
                textBox7.Text = "";
                textBox11.Text = "";
            }
            CalculateShopTotal();
        }

        private void ShopQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateShopTotal();
        }

        private void ShopPrice_TextChanged(object sender, EventArgs e)
        {
            CalculateShopTotal();
        }

        private void CalculateShopTotal()
        {
            decimal shopTotal = 0;

            if (checkBox1.Checked)
            {
                decimal price1, quantity1;
                if (decimal.TryParse(textBox4.Text, out price1) &&
                    decimal.TryParse(textBox8.Text, out quantity1))
                {
                    shopTotal += price1 * quantity1;
                }
            }

            if (checkBox2.Checked)
            {
                decimal price2, quantity2;
                if (decimal.TryParse(textBox5.Text, out price2) &&
                    decimal.TryParse(textBox9.Text, out quantity2))
                {
                    shopTotal += price2 * quantity2;
                }
            }

            if (checkBox3.Checked)
            {
                decimal price3, quantity3;
                if (decimal.TryParse(textBox6.Text, out price3) &&
                    decimal.TryParse(textBox10.Text, out quantity3))
                {
                    shopTotal += price3 * quantity3;
                }
            }

            if (checkBox4.Checked)
            {
                decimal price4, quantity4;
                if (decimal.TryParse(textBox7.Text, out price4) &&
                    decimal.TryParse(textBox11.Text, out quantity4))
                {
                    shopTotal += price4 * quantity4;
                }
            }

            label12.Text = shopTotal.ToString("F2");
        }

        #endregion



        #region Общий расчет

        private void Button1_Click(object sender, EventArgs e)
        {
            decimal fuelTotal = 0;
            decimal shopTotal = 0;
            decimal grandTotal = 0;

            if (decimal.TryParse(label11.Text, out fuelTotal))
            {
                grandTotal += fuelTotal;
            }

            if (decimal.TryParse(label12.Text, out shopTotal))
            {
                grandTotal += shopTotal;
            }

            label13.Text = grandTotal.ToString("F2");
        }

        #endregion
    }
}