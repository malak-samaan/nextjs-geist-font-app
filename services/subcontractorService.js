const Subcontractor = require('../models/Subcontractor');

async function createSubcontractor(data) {
  try {
    const subcontractor = await Subcontractor.create(data);
    return subcontractor;
  } catch (error) {
    console.error('Error creating subcontractor:', error);
    throw error;
  }
}

async function getSubcontractors() {
  try {
    return await Subcontractor.findAll();
  } catch (error) {
    console.error('Error fetching subcontractors:', error);
    throw error;
  }
}

async function updateSubcontractor(id, data) {
  try {
    const subcontractor = await Subcontractor.findByPk(id);
    if (!subcontractor) throw new Error('Subcontractor not found');
    await subcontractor.update(data);
    return subcontractor;
  } catch (error) {
    console.error('Error updating subcontractor:', error);
    throw error;
  }
}

async function deleteSubcontractor(id) {
  try {
    const subcontractor = await Subcontractor.findByPk(id);
    if (!subcontractor) throw new Error('Subcontractor not found');
    await subcontractor.destroy();
    return true;
  } catch (error) {
    console.error('Error deleting subcontractor:', error);
    throw error;
  }
}

module.exports = {
  createSubcontractor,
  getSubcontractors,
  updateSubcontractor,
  deleteSubcontractor,
};
