const Attachment = require('../models/Attachment');

async function uploadAttachment(data) {
  try {
    const attachment = await Attachment.create(data);
    return attachment;
  } catch (error) {
    console.error('Error uploading attachment:', error);
    throw error;
  }
}

async function getAttachments() {
  try {
    return await Attachment.findAll();
  } catch (error) {
    console.error('Error fetching attachments:', error);
    throw error;
  }
}

async function deleteAttachment(id) {
  try {
    const attachment = await Attachment.findByPk(id);
    if (!attachment) throw new Error('Attachment not found');
    await attachment.destroy();
    return true;
  } catch (error) {
    console.error('Error deleting attachment:', error);
    throw error;
  }
}

module.exports = {
  uploadAttachment,
  getAttachments,
  deleteAttachment,
};
