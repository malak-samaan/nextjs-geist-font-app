const { DataTypes } = require('sequelize');
const sequelize = require('./index');

const Attachment = sequelize.define('Attachment', {
  id: {
    type: DataTypes.INTEGER,
    autoIncrement: true,
    primaryKey: true,
  },
  fileName: {
    type: DataTypes.STRING,
    allowNull: false,
  },
  filePath: {
    type: DataTypes.STRING,
    allowNull: false,
  },
  uploadDate: {
    type: DataTypes.DATE,
    defaultValue: DataTypes.NOW,
  },
  associatedEntityType: {
    type: DataTypes.STRING,
  },
  associatedEntityId: {
    type: DataTypes.INTEGER,
  },
}, {
  timestamps: true,
  tableName: 'attachments',
});

module.exports = Attachment;
