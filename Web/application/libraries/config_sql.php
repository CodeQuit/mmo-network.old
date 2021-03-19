<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');
class config_sql
{
	/**
	* CodeIgniter global
	*
	* @var string
	**/
	protected $ci;

	public function __construct()
	{
		$this->ci =& get_instance();
		$this->ci->load->model('config_model');
	}
	
	public function config_load($data)
	{
		$_load = $this->ci->config_model->_read($data);
		return $_load;
	}
	
	public function config_load_all()
	{
		$_load_all = $this->ci->config_model->_read_ALL();
		return $_load_all;
	}
	
	public function config_save($data)
	{
		$_save = $this->ci->config_model->_save($data);
		return $_save;
	}
}