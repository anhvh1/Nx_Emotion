Del \\192.168.100.51\WebDeploy\Nx\Client\static\js
Del \\192.168.100.51\WebDeploy\Nx\Client\static\css
robocopy build \\192.168.100.51\WebDeploy\Nx\Client /E /IS /XF *.cs *.mp4 /XD factory
