const { DataTypes } = require('sequelize');
const sequelize = require('./index');

const Account = sequelize.define('Account', {
  id: {
    type: DataTypes.INTEGER,
    autoIncrement: true,
    primaryKey: true,
  },
  accountName: {
    type: DataTypes.STRING,
    allowNull: false,
  },
  accountCode: {
    type: DataTypes.STRING,
    allowNull: false,
    unique: true,
  },
  accountType: {
    type: DataTypes.STRING,
    allowNull: false,
  },
  balance: {
    type: DataTypes.DECIMAL(15, 2),
    defaultValue: 0.0,
  },
}, {
  timestamps: true,
  tableName: 'accounts',
});

module.exports = Account;
