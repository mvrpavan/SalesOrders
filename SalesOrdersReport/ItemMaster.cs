using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesOrdersReport
{
    class ItemDetails
    {
        public Int32 ID;
        public String ItemName, VendorName;
        public Double Price;
    }

    class VendorDetails2
    {
        public String VendorName;
        public System.Drawing.Color Color;
    }

    class CItemMaster
    {
        List<ItemDetails> ListItems;
        List<VendorDetails2> ListVendors;
        List<System.Drawing.Color> ListColors;

        public void Initialize()
        {
            try
            {
                ListItems = new List<ItemDetails>();
                ListVendors = new List<VendorDetails2>();

                ListColors = new List<System.Drawing.Color>();
                //ListColors.Add(System.Drawing.Color.FromArgb(242, 220, 219));
                ListColors.Add(System.Drawing.Color.FromArgb(184, 204, 228));
                ListColors.Add(System.Drawing.Color.FromArgb(218, 238, 243));
                ListColors.Add(System.Drawing.Color.FromArgb(216, 228, 188));
                ListColors.Add(System.Drawing.Color.FromArgb(228, 223, 236));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddToItemsList(Int32 ID, String ItemName, String VendorName, Double Price)
        {
            try
            {
                Int32 ItemIndex = ListItems.FindIndex(e => e.ID == ID);
                if (ItemIndex < 0)
                {
                    ItemIndex = ListItems.Count;
                    ItemDetails tmpItem = new ItemDetails();
                    tmpItem.ID = ID;
                    ListItems.Add(tmpItem);
                }
                ListItems[ItemIndex].ItemName = ItemName;
                ListItems[ItemIndex].VendorName = VendorName;
                ListItems[ItemIndex].Price = Price;
                AddToVendorList(VendorName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddToVendorList(String VendorName)
        {
            try
            {
                Int32 VendorIndx = ListVendors.FindIndex(e => e.VendorName.Equals(VendorName, StringComparison.InvariantCultureIgnoreCase));
                if (VendorIndx < 0)
                {
                    VendorDetails2 tmpVendor = new VendorDetails2();
                    tmpVendor.VendorName = VendorName;
                    tmpVendor.Color = ListColors[ListColors .Count % ListVendors.Count];
                    ListVendors.Add(tmpVendor);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
