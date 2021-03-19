package ru.mmo.global.dbc;

import java.sql.Array;
import java.sql.Blob;
import java.sql.CallableStatement;
import java.sql.Clob;
import java.sql.Connection;
import java.sql.DatabaseMetaData;
import java.sql.NClob;
import java.sql.PreparedStatement;
import java.sql.SQLClientInfoException;
import java.sql.SQLException;
import java.sql.SQLWarning;
import java.sql.SQLXML;
import java.sql.Savepoint;
import java.sql.Statement;
import java.sql.Struct;
import java.util.Map;
import java.util.Properties;
import java.util.concurrent.Executor;

/**
 * @author Felixx
 */
public class ConnectionWrapper implements Connection
{
	private Connection con;
	private long id;

	public ConnectionWrapper(Connection con)
	{
		if(con == null)
		{
			throw new IllegalArgumentException("connection can't be null");
		}
		this.con = con;
		this.id = System.currentTimeMillis();
	}

	public long getId()
	{
		return this.id;
	}

	@Override
	public String toString()
	{
		return "[" + this.id + "]: " + super.toString();
	}

	@Override
	public int hashCode()
	{
		return this.con.hashCode();
	}

	@Override
	public boolean equals(Object o)
	{
		return this.con.equals(o);
	}

	@Override
	public <T>T unwrap(Class<T> iface) throws SQLException
	{
		return this.con.unwrap(iface);
	}

	public boolean isWrapperFor(Class<?> iface) throws SQLException
	{
		return this.con.isWrapperFor(iface);
	}

	public Statement createStatement() throws SQLException
	{
		return this.con.createStatement();
	}

	public PreparedStatement prepareStatement(String sql) throws SQLException
	{
		return this.con.prepareStatement(sql);
	}

	public CallableStatement prepareCall(String sql) throws SQLException
	{
		return this.con.prepareCall(sql);
	}

	public String nativeSQL(String sql) throws SQLException
	{
		return this.con.nativeSQL(sql);
	}

	public void setAutoCommit(boolean autoCommit) throws SQLException
	{
		this.con.setAutoCommit(autoCommit);
	}

	public boolean getAutoCommit() throws SQLException
	{
		return this.con.getAutoCommit();
	}

	public void commit() throws SQLException
	{
		this.con.commit();
	}

	public void rollback() throws SQLException
	{
		this.con.rollback();
	}

	public void close() throws SQLException
	{
		this.con.close();
	}

	public boolean isClosed() throws SQLException
	{
		return this.con.isClosed();
	}

	public DatabaseMetaData getMetaData() throws SQLException
	{
		return this.con.getMetaData();
	}

	public void setReadOnly(boolean readOnly) throws SQLException
	{
		this.con.setReadOnly(readOnly);
	}

	public boolean isReadOnly() throws SQLException
	{
		return this.con.isReadOnly();
	}

	public void setCatalog(String catalog) throws SQLException
	{
		this.con.setCatalog(catalog);
	}

	public String getCatalog() throws SQLException
	{
		return this.con.getCatalog();
	}

	public void setTransactionIsolation(int level) throws SQLException
	{
		this.con.setTransactionIsolation(level);
	}

	public int getTransactionIsolation() throws SQLException
	{
		return this.con.getTransactionIsolation();
	}

	public SQLWarning getWarnings() throws SQLException
	{
		return this.con.getWarnings();
	}

	public void clearWarnings() throws SQLException
	{
		this.con.clearWarnings();
	}

	public Statement createStatement(int resultSetType, int resultSetConcurrency) throws SQLException
	{
		return this.con.createStatement(resultSetType, resultSetConcurrency);
	}

	public PreparedStatement prepareStatement(String sql, int resultSetType, int resultSetConcurrency) throws SQLException
	{
		return this.con.prepareStatement(sql, resultSetType, resultSetConcurrency);
	}

	public CallableStatement prepareCall(String sql, int resultSetType, int resultSetConcurrency) throws SQLException
	{
		return this.con.prepareCall(sql, resultSetType, resultSetConcurrency);
	}

	public Map<String, Class<?>> getTypeMap() throws SQLException
	{
		return this.con.getTypeMap();
	}

	public void setTypeMap(Map<String, Class<?>> map) throws SQLException
	{
		this.con.setTypeMap(map);
	}

	public void setHoldability(int holdability) throws SQLException
	{
		this.con.setHoldability(holdability);
	}

	public int getHoldability() throws SQLException
	{
		return this.con.getHoldability();
	}

	public Savepoint setSavepoint() throws SQLException
	{
		return this.con.setSavepoint();
	}

	public Savepoint setSavepoint(String name) throws SQLException
	{
		return this.con.setSavepoint(name);
	}

	public void rollback(Savepoint savepoint) throws SQLException
	{
		this.con.rollback(savepoint);
	}

	public void releaseSavepoint(Savepoint savepoint) throws SQLException
	{
		this.con.releaseSavepoint(savepoint);
	}

	public Statement createStatement(int resultSetType, int resultSetConcurrency, int resultSetHoldability) throws SQLException
	{
		return this.con.createStatement(resultSetType, resultSetConcurrency, resultSetHoldability);
	}

	public PreparedStatement prepareStatement(String sql, int resultSetType, int resultSetConcurrency, int resultSetHoldability) throws SQLException
	{
		return this.con.prepareStatement(sql, resultSetType, resultSetConcurrency, resultSetHoldability);
	}

	public CallableStatement prepareCall(String sql, int resultSetType, int resultSetConcurrency, int resultSetHoldability) throws SQLException
	{
		return this.con.prepareCall(sql, resultSetType, resultSetConcurrency, resultSetHoldability);
	}

	public PreparedStatement prepareStatement(String sql, int autoGeneratedKeys) throws SQLException
	{
		return this.con.prepareStatement(sql, autoGeneratedKeys);
	}

	public PreparedStatement prepareStatement(String sql, int[] columnIndexes) throws SQLException
	{
		return this.con.prepareStatement(sql, columnIndexes);
	}

	public PreparedStatement prepareStatement(String sql, String[] columnNames) throws SQLException
	{
		return this.con.prepareStatement(sql, columnNames);
	}

	public Clob createClob() throws SQLException
	{
		return this.con.createClob();
	}

	public Blob createBlob() throws SQLException
	{
		return this.con.createBlob();
	}

	public NClob createNClob() throws SQLException
	{
		return this.con.createNClob();
	}

	public SQLXML createSQLXML() throws SQLException
	{
		return this.con.createSQLXML();
	}

	public boolean isValid(int timeout) throws SQLException
	{
		return this.con.isValid(timeout);
	}

	public void setClientInfo(String name, String value) throws SQLClientInfoException
	{
		this.con.setClientInfo(name, value);
	}

	public void setClientInfo(Properties properties) throws SQLClientInfoException
	{
		this.con.setClientInfo(properties);
	}

	public String getClientInfo(String name) throws SQLException
	{
		return this.con.getClientInfo(name);
	}

	public Properties getClientInfo() throws SQLException
	{
		return this.con.getClientInfo();
	}

	public Array createArrayOf(String typeName, Object[] elements) throws SQLException
	{
		return this.con.createArrayOf(typeName, elements);
	}

	public Struct createStruct(String typeName, Object[] attributes) throws SQLException
	{
		return this.con.createStruct(typeName, attributes);
	}

    public void setSchema(String schema) throws SQLException {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public String getSchema() throws SQLException {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void abort(Executor executor) throws SQLException {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void setNetworkTimeout(Executor executor, int milliseconds) throws SQLException {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public int getNetworkTimeout() throws SQLException {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }
}