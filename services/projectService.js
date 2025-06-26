const Project = require('../models/Project');

async function createProject(data) {
  try {
    const project = await Project.create(data);
    return project;
  } catch (error) {
    console.error('Error creating project:', error);
    throw error;
  }
}

async function getProjects() {
  try {
    return await Project.findAll();
  } catch (error) {
    console.error('Error fetching projects:', error);
    throw error;
  }
}

async function updateProject(id, data) {
  try {
    const project = await Project.findByPk(id);
    if (!project) throw new Error('Project not found');
    await project.update(data);
    return project;
  } catch (error) {
    console.error('Error updating project:', error);
    throw error;
  }
}

async function deleteProject(id) {
  try {
    const project = await Project.findByPk(id);
    if (!project) throw new Error('Project not found');
    await project.destroy();
    return true;
  } catch (error) {
    console.error('Error deleting project:', error);
    throw error;
  }
}

module.exports = {
  createProject,
  getProjects,
  updateProject,
  deleteProject,
};
