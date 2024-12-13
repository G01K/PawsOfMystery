=== goto_forest ===
~ currentSpeaker = Characters.해설
족제비가 다스를 데려다 준 곳은 수풀이 우거진 산 아래의 작은 숲이었다.
~ currentSpeaker = Characters.족제비
햄스터는 이쪽에서 씨앗을 모으는 걸 좋아해. 위에 나무들이 많고, 떨어진 씨앗이나 
열매가 아래 수풀에 잘 숨겨져 있어서 먹을 게 많다고 좋아했어. 아마 이 근처일 거야.
~ currentSpeaker = Characters.주인공
마미랑 쿠크한테 잠깐 나갔다 온다고만 말하면 된다쭈.  
그다음에 너의 사라진 친구가 가장 마지막까지 있었던 장소로 데려다주라 쭈.
~ currentSpeaker = Characters.족제비
그래, 어디로 갔는지는 대강 알 것 같아.

* 킁킁 냄새를 맡아보자
    -> sniff_around
* 족제비에게 물어본다. 
    -> ask_weasel

=== sniff_around ===
~ currentSpeaker = Characters.해설
다스는 코를 킁킁거리며 주변 냄새를 탐지하기 시작했다.

+ 여우의 털을 발견했다.
    -> get_hintFoxFur
+ 공작의 깃털을 발견했다.
    -> get_hintPeacock
+ 햄스터가 뱉어낸 씨앗을 발견했다.
    -> get_hintSeeds
+ 염소의 발자국을 발견했다.
    -> get_hintFootPrint
+ 의미 없는 냄새 오브제 1을 발견했다.
    ~ currentSpeaker = Characters.주인공
    (킁킁... 이건 사건과 관련 없는 것 같쭈. 패스하자.)
    -> sniff_around

-> choose_home

=== ask_weasel ===
~ currentSpeaker = Characters.해설
다스는 족제비에게 물었다.
~ currentSpeaker = Characters.주인공
여기 말고도 햄스터가 좋아하던 장소가 있냐 쭈?
~ currentSpeaker = Characters.족제비
그런 건 없는 것 같아. 여기에서 항상 씨앗을 모으곤 했어.
다른 동물들하고 다투거나 이상한 일이 있었던 것도 아니야.
~ currentSpeaker = Characters.주인공
알겠다 쭈. 냄새로 단서를 찾아보자.

-> sniff_around

=== get_hintSeeds ===
~ currentSpeaker = Characters.해설
다스는 씨앗 더미에서 햄스터의 냄새를 맡았다.
~ currentSpeaker = Characters.주인공
족제비, 혹시 이 씨앗에서 나는 게 햄스터의 냄새가 맞냐 쭈?
~ currentSpeaker = Characters.족제비
킁킁, 맞아! 여기서 씨앗을 토하고 간 것 같아. 
누군가에게 납치당해서 다급했던 걸까?
~ currentSpeaker = Characters.주인공
이 근처에서 사건이 일어났나 보군.

-> sniff_around

=== get_hintFoxFur ===
~ currentSpeaker = Characters.해설
다스는 여우의 털을 발견했다.
-> goto_foxRoom

=== get_hintPeacock ===
~ currentSpeaker = Characters.해설
다스는 공작의 깃털을 발견했다.
-> goto_peacockRoom

=== get_hintFootPrint ===
~ currentSpeaker = Characters.해설
다스는 염소의 발자국을 발견했다.
-> goto_goatRoom

=== choose_home ===
//~ PauseStory() // Unity에서 Ink 스토리를 일시정지

-> gather_hints