const Supplier = require('../models/Supplier');

async function createSupplier(data) {
  try {
    const supplier = await Supplier.create(data);
    return supplier;
  } catch (error) {
    console.error('Error creating supplier:', error);
    throw error;
  }
}

async function getSuppliers() {
  try {
    return await Supplier.findAll();
  } catch (error) {
    console.error('Error fetching suppliers:', error);
    throw error;
  }
}

async function updateSupplier(id, data) {
  try {
    const supplier = await Supplier.findByPk(id);
    if (!supplier) throw new Error('Supplier not found');
    await supplier.update(data);
    return supplier;
  } catch (error) {
    console.error('Error updating supplier:', error);
    throw error;
  }
}

async function deleteSupplier(id) {
  try {
    const supplier = await Supplier.findByPk(id);
    if (!supplier) throw new Error('Supplier not found');
    await supplier.destroy();
    return true;
  } catch (error) {
    console.error('Error deleting supplier:', error);
    throw error;
  }
}

module.exports = {
  createSupplier,
  getSuppliers,
  updateSupplier,
  deleteSupplier,
};
