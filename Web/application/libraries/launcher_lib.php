<?php  if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class launcher_lib
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
		$this->ci->load->library('ion_auth');
		$this->ci->load->model('launcher_model');
	}

	public function getGameList()
	{
		$game_list = $this->ci->launcher_model->glist();
		return $game_list;
	}

	public function auth($login, $password, $version)
	{
		if ($this->ci->ion_auth->login($login, $password, false))
		{
			return 1;
		}
		else
		{
			return 2;
		}
	}

	public function News($gameid)
	{
		$news = $this->ci->launcher_model->news_model($gameid);
		return $news;
	}

}
?>