const Invoice = require('../models/Invoice');
const InvoiceItem = require('../models/InvoiceItem');

async function createInvoice(data) {
  try {
    const invoice = await Invoice.create(data, { include: [InvoiceItem] });
    return invoice;
  } catch (error) {
    console.error('Error creating invoice:', error);
    throw error;
  }
}

async function getInvoices() {
  try {
    return await Invoice.findAll({ include: InvoiceItem });
  } catch (error) {
    console.error('Error fetching invoices:', error);
    throw error;
  }
}

async function updateInvoice(id, data) {
  try {
    const invoice = await Invoice.findByPk(id);
    if (!invoice) throw new Error('Invoice not found');
    await invoice.update(data);
    return invoice;
  } catch (error) {
    console.error('Error updating invoice:', error);
    throw error;
  }
}

async function deleteInvoice(id) {
  try {
    const invoice = await Invoice.findByPk(id);
    if (!invoice) throw new Error('Invoice not found');
    await invoice.destroy();
    return true;
  } catch (error) {
    console.error('Error deleting invoice:', error);
    throw error;
  }
}

module.exports = {
  createInvoice,
  getInvoices,
  updateInvoice,
  deleteInvoice,
};
