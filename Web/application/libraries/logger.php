<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');
class logger
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
		$this->ci->load->model('logger_model');
	}
	
	public function writter($login, $text)
	{
		$log_w = $this->ci->logger_model->logs_writter_model($login, $text);
		return $log_w;
	}

	public function reader($login)
	{
		$log_r = $this->ci->logger_model->log_reader_model($login);
		return $log_r;
	}
	
	public function custom_log_reader($login = null, $limit = 500)
	{
		$log_r = $this->ci->logger_model->custom_log_reader_model($login);
		return $log_r;
	}
}