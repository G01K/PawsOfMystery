=== gather_hints ===
~ currentSpeaker = Characters.해설
용의자의 방에서 증거 수집을 마친 뒤, 다스는 족제비와 함께 상황을 정리하려 했다.
~ currentSpeaker = Characters.족제비
솔직히 나는 그 누구도 의심스럽지 않아...  
다들 그런 일을 할 동물들이 아닌걸.  
햄스터는 정말... 어디 있는 걸까.
~ currentSpeaker = Characters.주인공
모두 조금씩 수상한 구석은 있다쭈.
~ currentSpeaker = Characters.족제비
수풀에서 조사한 증거로 용의자를 추려서  
용의자의 집에서 조사를 마치고 나왔는데,  
의심 가는 동물이 있었어?

* 여우를 의심한다.
    -> suspect_fox
* 공작을 의심한다.
    -> suspect_peacock
* 염소를 의심한다.
    -> suspect_goat
* 햄스터를 의심한다.
    -> suspect_hamster
* 족제비를 의심한다.
    -> suspect_weasel

=== suspect_fox ===
~ currentSpeaker = Characters.주인공
여우는 처음부터 너무 방어적이었다쭈.  
그리고 현장에서 발견된 털도 여우의 것이었다쭈.
~ currentSpeaker = Characters.족제비
그런가? 하지만 여우는 항상 말투가 까칠해서...  
진짜로 뭔가 숨기는 건 아닐 수도 있지 않을까?
-> finalize_suspects

=== suspect_peacock ===
~ currentSpeaker = Characters.주인공
공작은 자신을 너무 꾸미는 데 집중한 것 같다쭈.  
그런데도 햄스터와 관련된 단서를 꽤 숨기려는 것처럼 보였쭈.
~ currentSpeaker = Characters.족제비
공작이 정말 그런 일을 했을까?  
평소에는 정말 친절한 친구였는데...
-> finalize_suspects

=== suspect_goat ===
~ currentSpeaker = Characters.주인공
염소는 뭔가 알고 있는 게 분명하다쭈.  
햄스터와 마지막으로 대화를 나눈 동물이기도 하고...
~ currentSpeaker = Characters.족제비
염소가 항상 현명하고 조용한 동물이라...  
햄스터를 다치게 했을 거라고는 상상이 안 돼.
-> finalize_suspects

=== suspect_hamster ===
~ currentSpeaker = Characters.주인공
햄스터가 혹시 스스로 무언가를 감추려 했던 건 아닐까쭈?  
단서들로 보면 햄스터 자신이 뭔가를 계획했던 것처럼 보이기도 해.
~ currentSpeaker = Characters.족제비
그럴 리가 없어! 햄스터는 정말 좋은 아이야.  
너무 무리한 추정 아닌가?
-> finalize_suspects

=== suspect_weasel ===
~ currentSpeaker = Characters.주인공
족제비, 너는 어떻게 생각하냐쭈?  
너도 현장에서 나온 단서들에 뭔가 이상한 점이 보이지 않았냐쭈?
~ currentSpeaker = Characters.족제비
뭐? 설마 나를 의심하는 거야?  
내가 그런 짓을 했을 리가 없잖아!  
정말 믿어주지 않는 거야?
-> finalize_suspects

=== finalize_suspects ===
~ currentSpeaker = Characters.해설
다스는 용의자에 대한 자신의 의견을 정리하며, 다음 단계로 나아가기로 결심했다.  
다음은 수집한 증거를 바탕으로 사건의 전말을 밝힐 차례였다.
-> choose_home