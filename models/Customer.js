const { DataTypes } = require('sequelize');
const sequelize = require('./index');

const Customer = sequelize.define('Customer', {
  id: {
    type: DataTypes.INTEGER,
    autoIncrement: true,
    primaryKey: true,
  },
  customerName: {
    type: DataTypes.STRING,
    allowNull: false,
  },
  contactNumber: {
    type: DataTypes.STRING,
  },
  email: {
    type: DataTypes.STRING,
    validate: {
      isEmail: true,
    },
  },
  address: {
    type: DataTypes.STRING,
  },
  creditLimit: {
    type: DataTypes.DECIMAL(15, 2),
    defaultValue: 0.0,
  },
}, {
  timestamps: true,
  tableName: 'customers',
});

module.exports = Customer;
