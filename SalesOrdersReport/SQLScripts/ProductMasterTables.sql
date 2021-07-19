--PriceGroupMaster
Insert into PRICEGROUPMASTER (PriceGroupName, Description, PriceColumn, Discount, DiscountType, IsDefault) Values('Retail Price', 'Retail Price', 'RetailPrice', 0, 'ABSOLUTE', 1);
Insert into PRICEGROUPMASTER (PriceGroupName, Description, PriceColumn, Discount, DiscountType, IsDefault) Values('Wholesale Price', 'Wholesale Price', 'WholesalePrice', 0, 'ABSOLUTE', 0);
Insert into PRICEGROUPMASTER (PriceGroupName, Description, PriceColumn, Discount, DiscountType, IsDefault) Values('Max Retail Price', 'Max Retail Price', 'MaxRetailPrice', 0, 'ABSOLUTE', 0);
Insert into PRICEGROUPMASTER (PriceGroupName, Description, PriceColumn, Discount, DiscountType, IsDefault) Values('Purchase Price', 'Purchase Price', 'PurchasePrice', 0, 'ABSOLUTE', 0);

--ProductCategoryMaster
Insert into ProductCategoryMaster (CategoryName, Description, Active) Values('Default', 'Default Category', 1);

--TaxMaster
-- Insert into TaxMaster (HSNCode, CGST, SGST, IGST) Values('1006', 2.5, 2.5, 0);
-- Insert into TaxMaster (HSNCode, CGST, SGST, IGST) Values('1102', 2.5, 2.5, 0);
-- Insert into TaxMaster (HSNCode, CGST, SGST, IGST) Values('1904', 0, 0, 0);
-- Insert into TaxMaster (HSNCode, CGST, SGST, IGST) Values('1513', 2.5, 2.5, 0);
-- Insert into TaxMaster (HSNCode, CGST, SGST, IGST) Values('904', 2.5, 2.5, 0);
-- Insert into TaxMaster (HSNCode, CGST, SGST, IGST) Values('909', 2.5, 2.5, 0);
-- Insert into TaxMaster (HSNCode, CGST, SGST, IGST) Values('910', 2.5, 2.5, 0);
-- Insert into TaxMaster (HSNCode, CGST, SGST, IGST) Values('2001', 2.5, 2.5, 0);
-- Insert into TaxMaster (HSNCode, CGST, SGST, IGST) Values('1515', 2.5, 2.5, 0);
-- Insert into TaxMaster (HSNCode, CGST, SGST, IGST) Values('2501', 2.5, 2.5, 0);
-- Insert into TaxMaster (HSNCode, CGST, SGST, IGST) Values('1101', 2.5, 2.5, 0);
-- Insert into TaxMaster (HSNCode, CGST, SGST, IGST) Values('1702', 2.5, 2.5, 0);
-- Insert into TaxMaster (HSNCode, CGST, SGST, IGST) Values('1902', 2.5, 2.5, 0);
-- Insert into TaxMaster (HSNCode, CGST, SGST, IGST) Values('201', 2.5, 2.5, 0);

--PaymentModeMaster
Insert into PaymentModeMaster (PaymentMode, Description) Values('Cash', 'Cash Payment');
Insert into PaymentModeMaster (PaymentMode, Description) Values('Debit Card', 'Debit Card Payment');
Insert into PaymentModeMaster (PaymentMode, Description) Values('Credit Card', 'Credit Card Payment');
Insert into PaymentModeMaster (PaymentMode, Description) Values('Cheque', 'Cheque Payment');
Insert into PaymentModeMaster (PaymentMode, Description) Values('UPI', 'UPI Payment');

--CustomerTypeMaster
Insert into CUSTOMERTYPEMASTER (CustomerType, Description) Values('Regular', 'Regular');
Insert into CUSTOMERTYPEMASTER (CustomerType, Description) Values('Retailer', 'Retailer');
Insert into CUSTOMERTYPEMASTER (CustomerType, Description) Values('Wholesaler', 'Wholesaler');

--ReportDefinedParams
INSERT INTO ReportDefinedParams (ParamID, ParamName, ActualColumnName, Type, DataType, TableNameToLookInto, IsPreDefinedParam) VALUES ('1','@CustomerName','CustomerName',	'String','String','CUSTOMERMASTER','1');
INSERT INTO ReportDefinedParams (ParamID, ParamName, ActualColumnName, Type, DataType, TableNameToLookInto, IsPreDefinedParam) VALUES ('2','@Amount','NETINVOICEAMOUNT','Value','Number','Invoices','1');
INSERT INTO ReportDefinedParams (ParamID, ParamName, ActualColumnName, Type, DataType, TableNameToLookInto, IsPreDefinedParam) VALUES ('3', '@InvoiceCreationDate', 'CreationDate', 'Date', 'string', 'Invoices', '1');
INSERT INTO ReportDefinedParams (ParamID, ParamName, ActualColumnName, Type, DataType, TableNameToLookInto, IsPreDefinedParam) VALUES ('4', '@OrderDate', 'CreationDate', 'Date', 'string', 'Orders', '1');
INSERT INTO ReportDefinedParams (ParamID, ParamName, ActualColumnName, Type, DataType, TableNameToLookInto, IsPreDefinedParam) VALUES ('5', '@InvoiceDate', 'InvoiceDate', 'Date', 'string', 'Invoices', '1');






