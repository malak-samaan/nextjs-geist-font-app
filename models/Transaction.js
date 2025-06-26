const { DataTypes } = require('sequelize');
const sequelize = require('./index');
const Account = require('./Account');

const Transaction = sequelize.define('Transaction', {
  id: {
    type: DataTypes.INTEGER,
    autoIncrement: true,
    primaryKey: true,
  },
  transactionType: {
    type: DataTypes.STRING,
    allowNull: false,
  },
  amount: {
    type: DataTypes.DECIMAL(15, 2),
    allowNull: false,
    validate: {
      min: 0.01,
    },
  },
  status: {
    type: DataTypes.STRING,
    allowNull: false,
  },
  date: {
    type: DataTypes.DATE,
    allowNull: false,
    defaultValue: DataTypes.NOW,
  },
  referenceNumber: {
    type: DataTypes.STRING,
  },
  accountId: {
    type: DataTypes.INTEGER,
    allowNull: false,
    references: {
      model: Account,
      key: 'id',
    },
  },
}, {
  timestamps: true,
  tableName: 'transactions',
});

Transaction.belongsTo(Account, { foreignKey: 'accountId' });
Account.hasMany(Transaction, { foreignKey: 'accountId' });

module.exports = Transaction;
