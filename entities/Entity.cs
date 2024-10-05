using Godot;
using System;

public partial class Entity : CharacterBody2D
{
	private uint id;
	
	public void setId(uint _id) 
	{
		this.id = _id;
	}
	
	public uint getId()
	{
		return this.id;
	}
}
