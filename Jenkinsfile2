projectname="gz-net-prod";
gitpath="http://github.com/WhyNoDream/MyTest.git";
workpath="/data/build/wms";///share/wms-jenkins
dllpath="/data/build/lib";//share/wms-jenkins/lib
version="v3.1";

//运行应用程序相关配置
applicationname="${params.applicationname}";//应用程序名
gitbranch="${params.gitbranch}";//git分支名称
evn="${params.evn}";//应用程序运行环境 例如 Development/Staging/Production
evnlowercase="${params.evn}".toLowerCase();//应用程序运行环境,小写
mybuildpath="${workpath}/${params.buildpath}";//执行编译目录 例如 /share/wms/src/Api/WMS.WebApi
myapplicationtype="${applicationtype}".toLowerCase();//应用程序类型 例如 Console/Web
myapplcationpoint=GetApplicationPoint(myapplicationtype);//应用程序端口 如果为控制台程序，则返回“”
buildnode=GetBuildNode(evn);//获取编译机器标签

//测试应用程序相关配置
testreportpath="IntegrationTesting";//测试报告
executetestpath="IntegrationTesting/TestRunner.py";//测试执行文件

//接收异常测试报告邮箱
toemail="caiy@tututech.net,cy_caiyan@qq.com";

/////// 编译构建（主要工作编译程序，生成镜像，将镜像推送到私有仓）
node (buildnode)
{
    stage('获取代码'){
        dir(workpath){
           git branch: gitbranch,
           url: gitpath
       }
       dir(workpath){
            sh '''git submodule init
            git submodule update'''
       }
    }
    stage('编译'){
        dir(mybuildpath){
            sh '''rm bin/publish -rf
            dotnet publish -c Release -f net6.0 -o bin/publish
            '''
        }
		dir(workpath){
			sh 'cp lib/ServiceStack.Text.dll ${buildpath}/bin/publish -f'
		}
    }
    stage('构建') {
        docker.withRegistry('http://106.52.59.92') {//--no-cache
            def customImage = docker.build("${projectname}/${applicationname}-${evnlowercase}:${version}",
            "  --build-arg ENVIRONMENT=${evn} ${mybuildpath}")
				customImage.push();
        }
    }
}
///////// end

///////// 部署（主要工作是从私有仓或者本地获取镜像，通过镜像启动程序）
if(evn=='Development'){
    node ('for_ubuntu_16'){
          DeployApplication();
    }
}
if(evn=='Staging'){
  node ('for_alpha_centos_16')
  {
       DeployApplication();
	   //ExecuteTest();
  }
}
if(evn=='Production'){
  if(myapplicationtype=='web'){
     node ("for_Ubuntu_224"){
        DeployApplication();
    }
  }
  node ("for_Ubuntu_223"){
    DeployApplication();
  }
}
//////// end

//////// 函数
//运行集成测试
def ExecuteTest(){
stage('集成测试') {
		dir(workpath){
			try{
				sh "python3 ${executetestpath}"
			}catch(e){
				println "集成测试异常!";
				SendEmailForException();
			}
			def repDir = "${testreportpath}/test_report";
			publishHTML([allowMissing: false,
			alwaysLinkToLastBuild: true,
			keepAll: true,
			reportDir: repDir,
			reportFiles: 'index.html',
			reportName: '测试报告'
			]);
		}
   }
}
//获取应用端口
def GetApplicationPoint(type){
	if(type=='web'){
		"${applcationpoint}";
	}else{
		"";
	}
}
//获取编译机器标签
def GetBuildNode(environmental){
	buildnode="for_alpha_centos_16";

	return buildnode;
}
//销毁现有容器
def DropContainer(){
	try{
        sh '''docker rm -f ${applicationname}'''
        sh '''docker images |grep ${applicationname} | awk '{ print $3 }' |xargs docker image rm'''
    }catch(e){
        echo "第一次构建${e}";
    }
}

//部署
def DeployApplication(){
	DropContainer();
    docker.withRegistry('http://106.52.59.92') {
       def image=docker.image("harbor.zhidiancloud.com/${projectname}/${applicationname}-${evnlowercase}:${version}");
       image.pull();
       def runstr='';
       if(myapplicationtype=='web'){
			runstr=" -v /usr/local/bin/:/usr/local/bin/ --name='${applicationname}' -m 1.5g --memory-swap=3G --restart=always -p ${myapplcationpoint}:80 --privileged";
		}else{
				runstr=" --name='${applicationname}' -m 1.5g --memory-swap=3G --restart=always  --privileged";
		}
            image.run(runstr);
       }	
}

def SendEmailForException(){
	 emailext( 
        body: '${FILE ,path="IntegrationTesting/test_report/index.html"}',
        mimeType: 'text/html',
        subject: "集成测试异常报告 ${projectname}/${applicationname}-${evnlowercase}:${version}",
       // to: toemail,
        recipientProviders: [[$class: 'CulpritsRecipientProvider'],[$class: 'DevelopersRecipientProvider']]
    )
}
