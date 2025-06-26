const SystemSettings = require('../models/SystemSettings');

async function getSettings() {
  try {
    return await SystemSettings.findAll();
  } catch (error) {
    console.error('Error fetching system settings:', error);
    throw error;
  }
}

async function updateSetting(key, value) {
  try {
    const setting = await SystemSettings.findOne({ where: { settingKey: key } });
    if (setting) {
      await setting.update({ settingValue: value });
      return setting;
    } else {
      const newSetting = await SystemSettings.create({ settingKey: key, settingValue: value });
      return newSetting;
    }
  } catch (error) {
    console.error('Error updating system setting:', error);
    throw error;
  }
}

module.exports = {
  getSettings,
  updateSetting,
};
