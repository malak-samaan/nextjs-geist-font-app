const { DataTypes } = require('sequelize');
const sequelize = require('./index');

const Supplier = sequelize.define('Supplier', {
  id: {
    type: DataTypes.INTEGER,
    autoIncrement: true,
    primaryKey: true,
  },
  supplierName: {
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
  paymentTerms: {
    type: DataTypes.STRING,
  },
}, {
  timestamps: true,
  tableName: 'suppliers',
});

module.exports = Supplier;
