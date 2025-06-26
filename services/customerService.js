const Customer = require('../models/Customer');

async function createCustomer(data) {
  try {
    const customer = await Customer.create(data);
    return customer;
  } catch (error) {
    console.error('Error creating customer:', error);
    throw error;
  }
}

async function getCustomers() {
  try {
    return await Customer.findAll();
  } catch (error) {
    console.error('Error fetching customers:', error);
    throw error;
  }
}

async function updateCustomer(id, data) {
  try {
    const customer = await Customer.findByPk(id);
    if (!customer) throw new Error('Customer not found');
    await customer.update(data);
    return customer;
  } catch (error) {
    console.error('Error updating customer:', error);
    throw error;
  }
}

async function deleteCustomer(id) {
  try {
    const customer = await Customer.findByPk(id);
    if (!customer) throw new Error('Customer not found');
    await customer.destroy();
    return true;
  } catch (error) {
    console.error('Error deleting customer:', error);
    throw error;
  }
}

module.exports = {
  createCustomer,
  getCustomers,
  updateCustomer,
  deleteCustomer,
};
