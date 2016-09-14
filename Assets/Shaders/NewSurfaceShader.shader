Shader "Plane/No zTest" { 
	SubShader{ 
		Pass{ 
		Blend SrcAlpha OneMinusSrcAlpha ZWrite On Cull Off Fog{ Mode Off } 
		BindChannels{ Bind "Color",color } } } }