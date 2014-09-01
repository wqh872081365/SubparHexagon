using UnityEngine;
using System.Collections;

public class SoundEffects : MonoBehaviour {
	public AudioClip powerupEffect;
	public AudioClip crashEffect;
	public AudioClip song;

	private static SoundEffects player;
	// Use this for initialization
	void Start () {
		player = this;
	}

	public static void playPowerupEffect(){
		player.audio.PlayOneShot(player.powerupEffect);
	}

	public static void playCrashEffect(){
		player.audio.PlayOneShot(player.crashEffect);
	}

	public static void playSong(){
		player.audio.PlayOneShot(player.song);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
