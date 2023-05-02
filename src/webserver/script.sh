#!/bin/bash

#criar as flags de acordo com o tipo de ação.
#Uso geral: script.sh <flag> <user>
#1 - criar usuario. -> script.sh 1 <usuario>
#2 - deletar usuario. -> script.sh 2 <usuario>
#3 - atualizar usuario <senha>. -> script.sh 3 <usuario> <senha>
#4 - desativar usuario. -> script.sh 4 <usuario>
#5 - ativar usuario. -> script.sh 5 <usuario>
#6 - adicionar usuario no grupo. script.sh 6 <usuario> <grupo> (O grupo deve existir).

#$1 = flag
#$2 = usuario (passwd --status root)
#$3 = senha
#$4 = desativar usuario (chage -l <usuario>)
#$5 = ativar usuario (chage -l <usuario>)
#$6 = adicionar usuario ao grupo. (cat /etc/group).
#$7 = Criar Grupo. (addgroup <nome do grupo>.

if [ $1 == 1 ] || [ $1 -eq "1" ]; then
/usr/sbin/useradd $2 --shell=/bin/false --no-create-home
/bin/echo $2":"$3 | chpasswd
(echo "$3"; echo "$3") | smbpasswd -a -s $2
systemctl restart smbd
fi

if [ $1 == 2 ] || [ $1 -eq "2" ]; then
/usr/bin/smbpasswd -x $2
/usr/sbin/userdel $2
systemctl restart smbd
fi

if [ $1 == 3 ] || [ $1 -eq "3" ]; then
(echo "$3"; echo "$3") | smbpasswd -a -s $2
/bin/echo $2":"$3 | chpasswd
systemctl restart smbd
fi

if [ $1 == 4 ] || [ $1 -eq "4" ]; then
/usr/sbin/usermod -s /sbin/nologin $2
/usr/sbin/usermod -L $2
/usr/bin/chage -E0 $2
#incluir controle de smb.
/usr/bin/smbpasswd -d $2
fi

if [ $1 == 5 ] || [ $1 -eq "5" ]; then
/usr/sbin/usermod -s /sbin/bin/false $2
/usr/sbin/usermod -U $2
/usr/bin/chage -E2050-01-01 $2
#incluir controle de smb.
/usr/bin/smbpasswd -e $2
fi

if [ $1 == 6 ] || [ $1 -eq "6" ]; then
/usr/sbin/usermod -a -G $3 $2
systemctl restart smbd
fi


if [ $1 == 7 ] || [ $1 -eq "7" ]; then
/usr/sbin/addgroup $2
systemctl restart smbd
fi



