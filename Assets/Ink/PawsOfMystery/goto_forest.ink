=== goto_forest
~ currentSpeaker = Characters.해설
족제비가 다스를 데려다 준 곳은 수풀이 우거진 산 아래의 작은 숲이었다.
~ currentSpeaker = Characters.족제비
햄스터는 이쪽에서 씨앗을 모으는 걸 좋아해. 위에 나무들이 많고, 떨어진 씨앗이나 
열매가 아래 수풀에 잘 숨겨져 있어서 먹을 게 많다고 좋아했어. 아마 이 근처일 거야.
~ currentSpeaker = Characters.주인공
마미랑 쿠크한테 잠깐 나갔다 온다고만 말하면 된다 쭈.  
그다음에 너의 사라진 친구가 가장 마지막까지 있었던 장소로 데려다주라 쭈.
~ currentSpeaker = Characters.족제비
그래, 어디로 갔는지는 대강 알 것 같아.

* 킁킁 냄새를 맡아보자
    -> sniff_around
* 족제비에게 물어본다. 
    -> ask_weasel
    
    
= sniff_around
<> { Inventory ? foxFur && foundFoxFur > 0}
<> { Inventory ? foxFur}
<> { foundFoxFur }


{  LIST_COUNT(Inventory) == 1 && Inventory ? none :
        ~ currentSpeaker = Characters.해설
        다스는 코를 킁킁거리며 주변 냄새를 탐지하기 시작했다.
        { StartButtonAnimation() }
        
   - else :
       {(foundFoxFur > 0 ||foundPeacockFeather > 0 || foundGoatFootprint  > 0  ):
            //다른 친구네 방으로 갔다가 왔다.
            ~ currentSpeaker = Characters.해설
            다스는 햄스터가 사라졌던 수풀로 돌아왔다.
            { StartButtonAnimation() }
        - else : 
            ~ currentSpeaker = Characters.주인공
            이게 뭐지?
       }
}

* { Inventory !? foxFur} [G1 여우의 털을 줍는다] //테스트
    ~ add_item(foxFur)
    -> goto_forest.sniff_around
* { Inventory !? peacockFeather} [G1 공작의 깃털을 줍는다] //테스트
    ~ add_item(peacockFeather)
    -> goto_forest.sniff_around
    
    
* { Inventory ? foxFur && foundFoxFur == 0} [수집한 증거를 확인한다.]
    ~ foundFoxFur = 1
    -> get_hintFoxFur
+ { Inventory ? foxFur && foundFoxFur > 0} [여우의 방으로간다.]
    { foundFoxFur == 1 :
        -> goto_foxRoom
    - else :
        -> goto_foxRoom.afterTalkFox
    }
    
    
* { Inventory ? peacockFeather && foundPeacockFeather == 0} [수집한 증거를 확인한다.]
    ~ foundPeacockFeather = 1
    -> get_hintPeacock
+ { Inventory ? peacockFeather && foundPeacockFeather > 0} [공작의 방으로간다.]
    { foundPeacockFeather == 1 :
        -> goto_peacockRoom
    - else :
        -> goto_peacockRoom.afterTalkPeacock
    }
    
    
* { Inventory ? goatFootprint && foundGoatFootprint == 0} [수집한 증거를 확인한다.]
    ~ foundGoatFootprint = 1
    -> get_hintFootPrint
+ { Inventory ? goatFootprint && foundGoatFootprint > 0} [염소의 방으로간다.]
    { foundGoatFootprint == 1 :
        -> goto_goatRoom
    - else :
        -> goto_goatRoom.afterTalkGoat
    }
    
// 1회차에는 무조건 필요함
* G1
-> choose_home

* { Inventory ? seeds } [수집한 증거를 확인한다.]
-> get_hintSeeds

 
     
= ask_weasel
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

= get_hintSeeds
~ currentSpeaker = Characters.해설
다스는 씨앗 더미에서 햄스터의 냄새를 맡았다.
~ currentSpeaker = Characters.주인공
족제비, 혹시 이 씨앗에서 나는 게 햄스터의 냄새가 맞냐 쭈?
~ currentSpeaker = Characters.족제비
킁킁, 맞아! 여기서 씨앗을 토하고 간 것 같아. 
누군가에게 납치당해서 다급했던 걸까?
~ currentSpeaker = Characters.주인공
이 근처에서 사건이 일어났나 보군.

{ StartButtonAnimation() }
-> goto_forest.sniff_around

= get_hintFoxFur
~ currentSpeaker = Characters.해설
다스는 여우의 털을 발견했다.
~ currentSpeaker = Characters.주인공
붉은 털이 있군 이게 뭔지알아?
~ currentSpeaker = Characters.족제비
여우의 털이야! 여우의 집으로가자!
-> sniff_around

= get_hintPeacock
~ currentSpeaker = Characters.해설
다스는 공작의 깃털을 발견했다.
~ currentSpeaker = Characters.족제비
공작의 집으로가자!
-> sniff_around

= get_hintFootPrint
~ currentSpeaker = Characters.해설
다스는 염소의 발자국을 발견했다.
~ currentSpeaker = Characters.족제비
염소 집으로가자!
-> sniff_around


=== choose_home ===
//~ PauseStory() // Unity에서 Ink 스토리를 일시정지
//    {(isGetAnyHintFoxRoom or isGetAnyHintPeacockRoom or isGetAnyHintGoatRoom):

    + [수풀로 돌아간다.]
    -> goto_forest.sniff_around

    { LIST_COUNT(Inventory) > 1 :
            * [결론을 낸다.]
              -> gather_hints
   } 
     
    
  