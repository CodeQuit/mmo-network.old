<?php defined('BASEPATH') OR exit('No direct script access allowed');

class Users extends CI_Controller 
{
	function __construct()
	{
		parent::__construct();
		$this->load->library('logger');
		$this->load->library('config_sql');
		$this->load->library('ion_auth');
		$this->load->library('session');
		$this->load->library('form_validation');
		$this->load->database();
		$this->load->helper('url');
		$this->load->helper('date');
	}

	//redirect if needed, otherwise display the user list
	function index()
	{
		$data['version'] = $this->config_sql->config_load('version');
		if($this->config->item('maintenance') == false)
		{
			$this->load->view('body', $data);
			if (!$this->ion_auth->logged_in())
			{
				//redirect them to the login page
				redirect('users/login', 'refresh');
			}
			elseif (!$this->ion_auth->is_admin())
			{
				//redirect them to the home page because they must be an administrator to view this
				redirect($this->config->item('base_url'), 'refresh');
			}
			else
			{
				//set the flash data error message if there is one
				$this->data['message'] = (validation_errors()) ? validation_errors() : $this->session->flashdata('message');

				//list the users
				$this->data['users'] = $this->ion_auth->users()->result();
				foreach ($this->data['users'] as $k => $user)
				{
					$this->data['users'][$k]->groups = $this->ion_auth->get_users_groups($user->id)->result();
				}
	
				$this->load->view('users/index', $this->data);
				$this->load->view('footer', $data);
			}
		}
		else
		{
			echo $this->config_sql->config_load('maintenance_text');
		}	
	}

	//log the user in
	function login()
	{
		$data['version'] = $this->config_sql->config_load('version');
		if($this->config->item('maintenance') == false)
		{
			$this->load->view('body', $data);
			$this->data['title'] = "Login";

			//validate form input
			$this->form_validation->set_rules('identity', 'Login', 'required');
			$this->form_validation->set_rules('password', 'Password', 'required');

			if ($this->form_validation->run() == true)
			{ 	//check to see if the user is logging in
				//check for "remember me"
				$remember = (bool) $this->input->post('remember');

				if ($this->ion_auth->login($this->input->post('identity'), $this->input->post('password'), $remember))
				{ 
				    //if the login is successful
					//redirect them back to the home page
					$this->session->set_flashdata('message', $this->ion_auth->messages());
					//$user_login = $this->input->post('identity');
					$Data_new = $this->ion_auth->user()->row();
					$login_log = $Data_new->username;
					$email_log = $Data_new->email;
					$ipaddress_log = $_SERVER["REMOTE_ADDR"];
					$this->logger->writter($login_log, "Вход успешно выполнен пользователем $login_log с ip адресом $ipaddress_log в личный кабинет.");
					redirect('/panel', 'refresh');
				
				}
				else
				{ 	//if the login was un-successful
					//redirect them back to the login page
					$this->session->set_flashdata('message', $this->ion_auth->errors());
					redirect('/users/login', 'refresh'); //use redirects instead of loading views for compatibility with MY_Controller libraries
				}
			}
			else
			{  //the user is not logging in so display the login page
				//set the flash data error message if there is one
				$this->data['message'] = (validation_errors()) ? validation_errors() : $this->session->flashdata('message');

				$this->data['identity'] = array(
					'name' => 'identity',
					'id' => 'identity',
					'type' => 'text',
					'value' => $this->form_validation->set_value('identity'),
				);
				$this->data['password'] = array(
					'name' => 'password',
					'id' => 'password',
					'type' => 'password',
				);

				$this->load->view('users/login', $this->data);
				$this->load->view('footer', $data);
			}
		}
		else
		{
			echo $this->config_sql->config_load('maintenance_text');
		}
	}

	//log the user out
	function logout()
	{
		$data['version'] = $this->config_sql->config_load('version');
		$this->data['title'] = "Logout";

		//log the user out
		$Data_new = $this->ion_auth->user()->row();
		$login_log = $Data_new->username;
		$email_log = $Data_new->email;
		$ipaddress_log = $_SERVER["REMOTE_ADDR"];
		$this->logger->writter($login_log, "Выход успешно выполнен пользователем $login_log c ip адресом $ipaddress_log из личного кабинета.");
		$logout = $this->ion_auth->logout();

		//redirect them back to the page they came from
		redirect('users', 'refresh');
	}

	//change password
	function change_password()
	{
		$data['version'] = $this->config_sql->config_load('version');
		$this->load->view('body', $data);
		$this->form_validation->set_rules('old', 'Old password', 'required');
		$this->form_validation->set_rules('new', 'New Password', 'required|min_length[' . $this->config->item('min_password_length', 'ion_auth') . ']|max_length[' . $this->config->item('max_password_length', 'ion_auth') . ']|matches[new_confirm]');
		$this->form_validation->set_rules('new_confirm', 'Confirm New Password', 'required');

		if (!$this->ion_auth->logged_in())
		{
			redirect('users/login', 'refresh');
		}
		
		$user = $this->ion_auth->user()->row();

		if ($this->form_validation->run() == false)
		{ //display the form
			//set the flash data error message if there is one
			$this->data['message'] = (validation_errors()) ? validation_errors() : $this->session->flashdata('message');

			$this->data['min_password_length'] = $this->config->item('min_password_length', 'ion_auth');
			$this->data['old_password'] = array(
				'name' => 'old',
				'id'   => 'old',
				'type' => 'password',
			);
			$this->data['new_password'] = array(
				'name' => 'new',
				'id'   => 'new',
				'type' => 'password',
				'pattern' => '^.{'.$this->data['min_password_length'].'}.*$',
			);
			$this->data['new_password_confirm'] = array(
				'name' => 'new_confirm',
				'id'   => 'new_confirm',
				'type' => 'password',
				'pattern' => '^.{'.$this->data['min_password_length'].'}.*$',
			);
			$this->data['user_id'] = array(
				'name'  => 'user_id',
				'id'    => 'user_id',
				'type'  => 'hidden',
				'value' => $user->id,
			);

			//render
			$this->load->view('users/change_password', $this->data);
			$this->load->view('footer', $data);
		}
		else
		{
			$identity = $this->session->userdata($this->config->item('identity', 'ion_auth'));

			$change = $this->ion_auth->change_password($identity, $this->input->post('old'), $this->input->post('new'));

			if ($change)
			{ //if the password was successfully changed
				$Data_new = $this->ion_auth->user()->row();
				$login_log = $Data_new->username;
				$email_log = $Data_new->email;
				$ipaddress_log = $_SERVER["REMOTE_ADDR"];
				
				$this->session->set_flashdata('message', $this->ion_auth->messages());
				
				$this->logger->writter($user_login, "Была выполнена смена пароля пользователем $login_log c ip адресом $ipaddress_log в личном кабинете.");
				$this->logout();
			}
			else
			{
				$this->session->set_flashdata('message', $this->ion_auth->errors());
				redirect('users/change_password', 'refresh');
			}
		}
	}

	//forgot password
	function forgot_password()
	{
		$data['version'] = $this->config_sql->config_load('version');
		$this->load->view('body', $data);
		$this->form_validation->set_rules('email', 'Email Address', 'required');
		if ($this->form_validation->run() == false)
		{
			//setup the input
			$this->data['email'] = array('name' => 'email',
				'id' => 'email',
			);
			//set any errors and display the form
			$this->data['message'] = (validation_errors()) ? validation_errors() : $this->session->flashdata('message');
			$this->load->view('users/forgot_password', $this->data);
			$this->load->view('footer', $data);
		}
		else
		{
			//run the forgotten password method to email an activation code to the user
			$forgotten = $this->ion_auth->forgotten_password($this->input->post('email'));

			if ($forgotten)
			{ //if there were no errors
				$this->session->set_flashdata('message', $this->ion_auth->messages());
				redirect("users/login", 'refresh'); //we should display a confirmation page here instead of the login page
			}
			else
			{
				$this->session->set_flashdata('message', $this->ion_auth->errors());
				redirect("users/forgot_password", 'refresh');
			}
		}
	}

	//reset password - final step for forgotten password
	public function reset_password($code)
	{
		$data['version'] = $this->config_sql->config_load('version');
		$reset = $this->ion_auth->forgotten_password_complete($code);

		if ($reset)
		{  //if the reset worked then send them to the login page
			$this->session->set_flashdata('message', $this->ion_auth->messages());
			$this->logger->writter($user_login, "Был сброшен пароль пользователя $user_login.");
			redirect("users/login", 'refresh');
		}
		else
		{ //if the reset didnt work then send them back to the forgot password page
			$this->session->set_flashdata('message', $this->ion_auth->errors());
			redirect("users/forgot_password", 'refresh');
		}
	}

	//activate the user
	function activate($id, $code=false)
	{
		$data['version'] = $this->config_sql->config_load('version');

		if ($code !== false)
			$activation = $this->ion_auth->activate($id, $code);
		else if ($this->ion_auth->is_admin())
			$activation = $this->ion_auth->activate($id);

		if ($activation)
		{
			//redirect them to the auth page
			$this->session->set_flashdata('message', $this->ion_auth->messages());
			redirect("admin/userlist", 'refresh');
		}
		else
		{
			//redirect them to the forgot password page
			$this->session->set_flashdata('message', $this->ion_auth->errors());
			redirect("users/forgot_password", 'refresh');
		}
	}

	//deactivate the user
	function deactivate($id = NULL)
	{
		$data['version'] = $this->config_sql->config_load('version');
		$this->load->view('body', $data);
		// no funny business, force to integer
		$id = (int) $id;

		$this->load->library('form_validation');
		$this->form_validation->set_rules('confirm', 'confirmation', 'required');
		$this->form_validation->set_rules('id', 'user ID', 'required|is_natural');

		if ($this->form_validation->run() == FALSE)
		{
			// insert csrf check
			$this->data['csrf'] = $this->_get_csrf_nonce();
			$this->data['user'] = $this->ion_auth->user($id)->row();

			$this->load->view('users/deactivate_user', $this->data);
			$this->load->view('footer', $data);
		}
		else
		{
			// do we really want to deactivate?
			if ($this->input->post('confirm') == 'yes')
			{
				// do we have a valid request?
				if ($this->_valid_csrf_nonce() === FALSE || $id != $this->input->post('id'))
				{
					show_404();
				}

				// do we have the right userlevel?
				if ($this->ion_auth->logged_in() && $this->ion_auth->is_admin())
				{
					if($id != null)
						$this->ion_auth->deactivate($id);
				}
			}
			//redirect them back to the auth page
			redirect('/admin/userlist', 'refresh');
		}
	}

	//create a new user
	function create_user()
	{
		$data['version'] = $this->config_sql->config_load('version');
		$this->data['title'] = "Create User";
		$this->load->view('body', $data);

		if ($this->ion_auth->logged_in())
		{
			redirect('user/panel', 'refresh');
		}

		//validate form input
		$this->form_validation->set_rules('username', 'UserName', 'required|xss_clean');
		$this->form_validation->set_rules('first_name', 'Имя', 'required|xss_clean');
		$this->form_validation->set_rules('last_name', 'Фамилия', 'required|xss_clean');
		$this->form_validation->set_rules('email', 'email', 'required|valid_email');
		$this->form_validation->set_rules('phone1', 'Мобильный телефон', 'required|xss_clean|min_length[11]|max_length[11]');
		$this->form_validation->set_rules('password', 'Пароль', 'required|min_length[' . $this->config->item('min_password_length', 'ion_auth') . ']|max_length[' . $this->config->item('max_password_length', 'ion_auth') . ']|matches[password_confirm]');
		$this->form_validation->set_rules('password_confirm', 'Повтор пароля', 'required');

		if ($this->form_validation->run() == true)
		{
			$username =  $this->input->post('username');//strtolower($this->input->post('first_name')) . ' ' . strtolower($this->input->post('last_name'));
			$email = $this->input->post('email');
			$password = $this->input->post('password');

			$additional_data = array(
				'first_name' => $this->input->post('first_name'),
				'last_name' => $this->input->post('last_name'),
				//'company' => $this->input->post('company'),
				'company' => 'not_used',
				'phone' => $this->input->post('phone1'),
			);
		}
		if ($this->form_validation->run() == true && $this->ion_auth->register($username, $password, $email, $additional_data))
		{ //check to see if we are creating the user
			//redirect them back to the admin page
			$this->session->set_flashdata('message', "User Created");
			redirect("users", 'refresh');
		}
		else
		{ //display the create user form
			//set the flash data error message if there is one
			$this->data['message'] = (validation_errors() ? validation_errors() : ($this->ion_auth->errors() ? $this->ion_auth->errors() : $this->session->flashdata('message')));
			
			$this->data['username'] = array('name' => 'username',
				'id' => 'username',
				'type' => 'text',
				'value' => $this->form_validation->set_value('username'),
			);

			$this->data['first_name'] = array('name' => 'first_name',
				'id' => 'first_name',
				'type' => 'text',
				'value' => $this->form_validation->set_value('first_name'),
			);
			$this->data['last_name'] = array('name' => 'last_name',
				'id' => 'last_name',
				'type' => 'text',
				'value' => $this->form_validation->set_value('last_name'),
			);
			$this->data['email'] = array('name' => 'email',
				'id' => 'email',
				'type' => 'text',
				'value' => $this->form_validation->set_value('email'),
			);
			//$this->data['company'] = array('name' => 'company',
			//	'id' => 'company',
			//	'type' => 'text',
			//	'value' => $this->form_validation->set_value('company'),
			//);
			$this->data['phone1'] = array('name' => 'phone1',
				'id' => 'phone1',
				'type' => 'text',
				'value' => $this->form_validation->set_value('phone1'),
			);
			//$this->data['phone2'] = array('name' => 'phone2',
			//	'id' => 'phone2',
			//	'type' => 'text',
			//	'value' => $this->form_validation->set_value('phone2'),
			//);
			//$this->data['phone3'] = array('name' => 'phone3',
			//	'id' => 'phone3',
			//	'type' => 'text',
			//	'value' => $this->form_validation->set_value('phone3'),
			//);
			$this->data['password'] = array('name' => 'password',
				'id' => 'password',
				'type' => 'password',
				'value' => $this->form_validation->set_value('password'),
			);
			$this->data['password_confirm'] = array('name' => 'password_confirm',
				'id' => 'password_confirm',
				'type' => 'password',
				'value' => $this->form_validation->set_value('password_confirm'),
			);
			$this->load->view('users/create_user', $this->data);
			$this->load->view('footer', $data);
		}
	}

	function _get_csrf_nonce()
	{
		$this->load->helper('string');
		$key = random_string('alnum', 8);
		$value = random_string('alnum', 20);
		$this->session->set_flashdata('csrfkey', $key);
		$this->session->set_flashdata('csrfvalue', $value);

		return array($key => $value);
	}

	function _valid_csrf_nonce()
	{
		if ($this->input->post($this->session->flashdata('csrfkey')) !== FALSE &&
				$this->input->post($this->session->flashdata('csrfkey')) == $this->session->flashdata('csrfvalue'))
		{
			return TRUE;
		}
		else
		{
			return FALSE;
		}
	}

}
