const { DataTypes } = require('sequelize');
const sequelize = require('./index');
const Invoice = require('./Invoice');

const InvoiceItem = sequelize.define('InvoiceItem', {
  id: {
    type: DataTypes.INTEGER,
    autoIncrement: true,
    primaryKey: true,
  },
  invoiceId: {
    type: DataTypes.INTEGER,
    allowNull: false,
    references: {
      model: Invoice,
      key: 'id',
    },
  },
  description: {
    type: DataTypes.STRING,
    allowNull: false,
  },
  quantity: {
    type: DataTypes.INTEGER,
    allowNull: false,
    validate: {
      min: 1,
    },
  },
  unitPrice: {
    type: DataTypes.DECIMAL(15, 2),
    allowNull: false,
  },
  tax: {
    type: DataTypes.DECIMAL(15, 2),
    defaultValue: 0.0,
  },
  discount: {
    type: DataTypes.DECIMAL(15, 2),
    defaultValue: 0.0,
  },
  lineTotal: {
    type: DataTypes.DECIMAL(15, 2),
    allowNull: false,
  },
}, {
  timestamps: true,
  tableName: 'invoice_items',
});

InvoiceItem.belongsTo(Invoice, { foreignKey: 'invoiceId' });
Invoice.hasMany(InvoiceItem, { foreignKey: 'invoiceId' });

module.exports = InvoiceItem;
