<?php  if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class launcher_model extends CI_Model
{
	public function glist()
	{
		$line = 0;
		$rows = array();
		$this->load->database();

		$num_query = $this->db->query("SELECT * FROM game_list;");
		$num = $num_query->num_rows();

		$query = $this->db->query("SELECT id, name, exename, argument ,status FROM game_list;");
		$opcode = 2025;
		$data_enc = '{"opcode": ' . $opcode . ', "ids_id": ' . $num . ' , "gamelist" : [';
		foreach ($query->result() as $row)
		{
			$line++;
    		$rows[$line]['id'] = $row->id;
			$rows[$line]['name'] = $row->name;
			$rows[$line]['exename'] = $row->exename;
			$rows[$line]['argument'] = $row->argument;
			$rows[$line]['status'] = $row->status;
			$data_enc = $data_enc .  json_encode($rows[$line]);
			if($line < $num)
			{
				$data_enc = $data_enc . ", ";
			}
		}
		$data_enc = $data_enc . "]}";
		return $data_enc;
	}



	public function news_model($gameid)
	{
		$this->load->database();

		$query = $this->db->query("SELECT text, date, gameid FROM service_news WHERE gameid='$gameid' LIMIT 1;");
		$opcode = 2027;

		foreach ($query->result() as $row)
		{
			$news['opcode'] = '2027';
			$news['news'] = $row->text;
			$news['date'] = $row->date;
			$data_enc = json_encode($news);
		}
		return $data_enc;
	}
}
?>