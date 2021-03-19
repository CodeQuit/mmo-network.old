<?php  if ( ! defined('BASEPATH')) exit('No direct script access allowed');
/*
 * @autor: DarkSkeleton
 */
class support_model extends CI_Model
{
	public function getQuestion($account)
	{
		$this->load->database();
		$query = $this->db->query("SELECT ticket, account, question, status FROM support_question WHERE account='$account';");
		foreach ($query->result() as $row)
		{
			$data = $data . "<tr><td>#$row->ticket</td><td>$row->question</td><td>$row->status</td></tr>";
		}
		return $data;
	}
	
	public function addQuestion($question, $account, $status, $gameid)
	{
		$this->load->database();
		$query = $this->db->query("INSERT INTO support_question (account, gameid, question, status) VALUES ('$account', '$gameid', '$question', '$status');");
	}
}
?>