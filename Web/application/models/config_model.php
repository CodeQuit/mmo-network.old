<?php  if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class config_model extends CI_Model
{
	public function __construct()
	{
		parent::__construct();
		$this->load->database();
	}

	/**
	* Сохранение конфигурации
	**/
	public function _save($data)
	{
		$_value = $data['value'];
		$_name = $data['name'];
		if($_name != null && $_value != null)
		{
			$this->db->query("UPDATE config SET value = '$_value' WHERE name = '$_name';");
			echo '>>>' . "Saving...";
			return true;
		}
		else
		{
			echo '>>>' . "No...";
			return false;
		}
	}

	/**
	* Чтение конфигурации
	**/
	public function _read($name)
	{
		$config = "";
		$query = $this->db->query("SELECT name, value FROM config WHERE name = '$name' LIMIT 1;");
		foreach ($query->result() as $row)
		{
			$config = $row->value;
		}
		return $config;
	}

	public function _read_ALL()
	{
		$config_all = "<tr><td>Параметр</td><td>Значение</td><td>Коментарий</td><td>Действие</td></tr>";
		$query = $this->db->query("SELECT name, value, comment FROM config;");
		foreach ($query->result() as $row)
		{
			$config_all = $config_all . "<tr><td>$row->name</td><td>$row->value</td><td>$row->comment</td><td><a href='/admin/conf_edit/$row->name'>Изменить</a></td></tr>";
		}
		return $config_all;
	}
}