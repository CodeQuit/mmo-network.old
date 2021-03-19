<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Panel extends CI_Controller
{

	function __construct()
	{
		parent::__construct();
		$this->load->library('logger');
		$this->load->library('config_sql');
		$this->load->library('ion_auth');
		$this->load->library('session');
		$this->load->library('game');
		$this->load->library('news');
		$this->load->library('form_validation');
		$this->load->database();
		$this->load->helper('url');
	}

	public function index()
	{
		$data['version'] = $this->config_sql->config_load('version');
		if($this->config_sql->config_load('maintenance') == "false")
		{
			if($this->ion_auth->logged_in())
			{
				$data_info = $this->ion_auth->user()->row();
				$data['ipaddress'] = $_SERVER["REMOTE_ADDR"];
				$data['status'] = $this->game->getStatusServer();
				$data['news'] = $this->news->getNews();
				$data['sysinfo'] = "";
				$group_array = $this->ion_auth->get_users_groups()->result();
				$data['group_info'] = $group_array['0']->description;
				$data['game_list'] = $this->game->getGameList($data_info->username);
				if(!$this->ion_auth->is_admin()){
					//TODO Дополнительные данные!
				}

				$this->load->view('body', $data);
				$this->load->view('/panel/panel_account_info', $data_info);
				$this->load->view('/panel/panel_form', $data);
				$this->load->view('footer', $data);
			}
			else
			{
				$this->load->view('body', $data);
				$this->load->view('/panel/not_auth');
				$this->load->view('footer', $data);
			}
		}
		else
		{
			echo $this->config_sql->config_load('maintenance_text');
		}
	}

	public function ServiceCreate($gameid)
	{
		$data['version'] = $this->config_sql->config_load('version');
		if($this->config_sql->config_load('maintenance') == "false")
		{
			if($this->ion_auth->logged_in())
			{

				$data_info = $this->ion_auth->user()->row();
				$data_status = $this->game->CreateAccount($data_info->username, $data_info->password, $gameid, 0);
				if($data_status == true)
				{
					$data['sysinfo'] = "<center>Пожалуйста подождите...</center><center>Создание аккаунта...</center><center>Аккаунт будет активирован через 2 часа.</center>";
					$data['redirect'] = '<meta http-equiv="refresh" content="3;URL=/panel">';
					$this->load->view('body', $data);
					$this->load->view('/panel/infomessage', $data);
					$this->load->view('footer', $data);
				}
				else
				{
					$data['sysinfo'] = "<center>Аккаунт уже существует!</center>";
					$data['redirect'] = '<meta http-equiv="refresh" content="3;URL=/panel">';
					$this->load->view('body', $data);
					$this->load->view('/panel/infomessage', $data);
					$this->load->view('footer', $data);
				}

			}
			else
			{
				$this->load->view('body', $data);
				$this->load->view('not_auth');
				$this->load->view('footer', $data);
			}
		}
		else
		{
			echo $this->config_sql->config_load('maintenance_text');
		}
	}


	public function profile()
	{
		$data['version'] = $this->config_sql->config_load('version');
		if($this->config_sql->config_load('maintenance') == "false")
		{
			$this->load->view('body', $data);
			if($this->ion_auth->logged_in())
			{
				$profile = $this->ion_auth->user()->row();
				$this->load->view('/panel/profile_page', $profile);
				$this->load->view('footer', $data);
			}
			else
			{
				$this->load->view('not_auth');
				$this->load->view('footer', $data);
			}
		}
		else
		{
			echo $this->config_sql->config_load('maintenance_text');
		}
	}

	public function user_logs()
	{
		$data['version'] = $this->config_sql->config_load('version');
		if($this->config_sql->config_load('maintenance') == "false")
		{
			$this->load->view('body', $data);

			if($this->ion_auth->logged_in())
			{
				$data_info = $this->ion_auth->user()->row();
				$logg['logs_user'] = $this->logger->reader($data_info->username);
				$this->load->view('/panel/log_user', $logg);
			}
			else
			{
				$this->load->view('not_auth');
			}
			$this->load->view('footer', $data);
		}
		else
		{
			echo $this->config_sql->config_load('maintenance_text');
		}
	}

	/**
	 *  Update user
	 *  @author Andrey V. Ponomarenko
	 */
    public function profile_update(){
		$data['version'] = $this->config_sql->config_load('version');
		if($this->config_sql->config_load('maintenance') == "false")
		{
            if($this->ion_auth->logged_in()){
                $user_id = $this->input->post('user_id');
                    if ($this->ion_auth->id_check($user_id)){
                    $this->form_validation->set_rules('first_name', 'Имя', 'required');
                    $this->form_validation->set_rules('last_name', 'Фамилия', 'required');
                    $this->form_validation->set_rules('email', 'E-mail', 'required|valid_email');
                    $this->form_validation->set_rules('phone', 'Телефон', 'required|min_length[11]|max_length[11]');
                    if ($this->form_validation->run() == true){
                       $user = $this->ion_auth->user($user_id)->row();
                        $data = array(
                            'first_name' => $this->input->post('first_name'),
                            'last_name' => $this->input->post('last_name'),
                            'phone' => $this->input->post('phone'),
                            'email' => $this->input->post('email'),
                        );
                        $this->ion_auth->update($user_id,$data);
                        $this->logger->writter($user->username, "Пользователь <b>".$user->username."</b> изменил свои данные" );
                        redirect('/panel/profile', 'refresh');
                    }else{
                        $data['version'] = $this->config->item('version');
                        $this->load->view('body', $data);
                        $profile = $this->ion_auth->user()->row();
                        $profile->message = (validation_errors() ? validation_errors() : ($this->ion_auth->errors() ? $this->ion_auth->errors() : $this->session->flashdata('message')));
                        $this->load->view('/panel/profile_page', $profile);
                        $this->load->view('footer', $data);
                    }
                }else{
                	$this->session->set_flashdata('message', $this->ion_auth->errors());
                	redirect('/panel/profile/', 'refresh');
                }
            }else{
                $this->load->view('body', $data);
                $data['version'] = $this->config->item('version');
                $this->load->view('not_auth');
                $this->load->view('footer', $data);
            }
        }else{
			echo $this->config_sql->config_load('maintenance_text');
        }
    }

}