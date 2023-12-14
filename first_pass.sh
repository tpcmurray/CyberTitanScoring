#! /usr/bin/bash
echo "Disabling root password …"
echo $(passwd -l root)
echo "Enabling firewall …"
echo $(ufw enable)
echo "Installing OpenSSH …"
echo $(apt-get install openssh-server)
echo "Starting SSH service …"
echo $(systemctl start sshd)
echo "Setting correct permissions on shadow file …"
echo $(chmod 640 /etc/shadow)
echo "Enabling stack and heap address space layout randomization  …"
echo $(sysctl -w kernel.randomize_va_space=2 )
echo "Fetching updated app data …"
echo $(apt update)
echo "Upgrading apps …"
echo $(apt upgrade)
