const { DataTypes } = require('sequelize');
const sequelize = require('./index');

const SystemSettings = sequelize.define('SystemSettings', {
  id: {
    type: DataTypes.INTEGER,
    autoIncrement: true,
    primaryKey: true,
  },
  settingKey: {
    type: DataTypes.STRING,
    allowNull: false,
    unique: true,
  },
  settingValue: {
    type: DataTypes.STRING,
  },
  description: {
    type: DataTypes.STRING,
  },
}, {
  timestamps: true,
  tableName: 'system_settings',
});

module.exports = SystemSettings;
