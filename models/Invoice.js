const { DataTypes } = require('sequelize');
const sequelize = require('./index');
const Customer = require('./Customer');

const Invoice = sequelize.define('Invoice', {
  id: {
    type: DataTypes.INTEGER,
    autoIncrement: true,
    primaryKey: true,
  },
  invoiceNumber: {
    type: DataTypes.STRING,
    allowNull: false,
    unique: true,
  },
  date: {
    type: DataTypes.DATE,
    allowNull: false,
    defaultValue: DataTypes.NOW,
  },
  customerId: {
    type: DataTypes.INTEGER,
    references: {
      model: Customer,
      key: 'id',
    },
  },
  totalAmount: {
    type: DataTypes.DECIMAL(15, 2),
    defaultValue: 0.0,
  },
  tax: {
    type: DataTypes.DECIMAL(15, 2),
    defaultValue: 0.0,
  },
  discount: {
    type: DataTypes.DECIMAL(15, 2),
    defaultValue: 0.0,
  },
  status: {
    type: DataTypes.STRING,
    defaultValue: 'Draft',
  },
}, {
  timestamps: true,
  tableName: 'invoices',
});

Invoice.belongsTo(Customer, { foreignKey: 'customerId' });
Customer.hasMany(Invoice, { foreignKey: 'customerId' });

module.exports = Invoice;
