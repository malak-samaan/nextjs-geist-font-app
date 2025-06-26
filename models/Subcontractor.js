const { DataTypes } = require('sequelize');
const sequelize = require('./index');

const Subcontractor = sequelize.define('Subcontractor', {
  id: {
    type: DataTypes.INTEGER,
    autoIncrement: true,
    primaryKey: true,
  },
  subcontractorName: {
    type: DataTypes.STRING,
    allowNull: false,
  },
  contact: {
    type: DataTypes.STRING,
  },
  email: {
    type: DataTypes.STRING,
    validate: {
      isEmail: true,
    },
  },
  specialty: {
    type: DataTypes.STRING,
  },
}, {
  timestamps: true,
  tableName: 'subcontractors',
});

module.exports = Subcontractor;
