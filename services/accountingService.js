const Account = require('../models/Account');
const Transaction = require('../models/Transaction');

async function createAccount(data) {
  try {
    const account = await Account.create(data);
    return account;
  } catch (error) {
    console.error('Error creating account:', error);
    throw error;
  }
}

async function getAccounts() {
  try {
    return await Account.findAll();
  } catch (error) {
    console.error('Error fetching accounts:', error);
    throw error;
  }
}

async function updateAccount(id, data) {
  try {
    const account = await Account.findByPk(id);
    if (!account) throw new Error('Account not found');
    await account.update(data);
    return account;
  } catch (error) {
    console.error('Error updating account:', error);
    throw error;
  }
}

async function deleteAccount(id) {
  try {
    const account = await Account.findByPk(id);
    if (!account) throw new Error('Account not found');
    await account.destroy();
    return true;
  } catch (error) {
    console.error('Error deleting account:', error);
    throw error;
  }
}

async function createTransaction(data) {
  try {
    const transaction = await Transaction.create(data);
    return transaction;
  } catch (error) {
    console.error('Error creating transaction:', error);
    throw error;
  }
}

async function getTransactions() {
  try {
    return await Transaction.findAll({ include: Account });
  } catch (error) {
    console.error('Error fetching transactions:', error);
    throw error;
  }
}

async function updateTransaction(id, data) {
  try {
    const transaction = await Transaction.findByPk(id);
    if (!transaction) throw new Error('Transaction not found');
    await transaction.update(data);
    return transaction;
  } catch (error) {
    console.error('Error updating transaction:', error);
    throw error;
  }
}

async function deleteTransaction(id) {
  try {
    const transaction = await Transaction.findByPk(id);
    if (!transaction) throw new Error('Transaction not found');
    await transaction.destroy();
    return true;
  } catch (error) {
    console.error('Error deleting transaction:', error);
    throw error;
  }
}

module.exports = {
  createAccount,
  getAccounts,
  updateAccount,
  deleteAccount,
  createTransaction,
  getTransactions,
  updateTransaction,
  deleteTransaction,
};
