projectname="gz-net-prod";
gitpath="http://github.com/WhyNoDream/MyTest.git";
workpath="/root/testpath";///share/wms-jenkins
dllpath="/root/testpath/lib";//share/wms-jenkins/lib
version="v3.1";

//运行应用程序相关配置
applicationname="test";//"${params.applicationname}";//应用程序名
gitbranch="master";//"${params.gitbranch}";//git分支名称
evn="Development";//"${params.evn}";//应用程序运行环境 例如 Development/Staging/Production
evnlowercase="development";//"${params.evn}".toLowerCase();//应用程序运行环境,小写
mybuildpath="/share/wms/src/Api/WMS.WebApi";//"${workpath}/${params.buildpath}";//执行编译目录 例如 /share/wms/src/Api/WMS.WebApi
myapplicationtype="Web";//"${applicationtype}".toLowerCase();//应用程序类型 例如 Console/Web
myapplcationpoint=GetApplicationPoint(myapplicationtype);//应用程序端口 如果为控制台程序，则返回“”
buildnode=GetBuildNode(evn);//获取编译机器标签

node
{
    stage('获取代码'){
        dir(workpath){
           git branch: gitbranch,
           url: gitpath
       }
    }
    stage('编译'){
        dir(mybuildpath){
            sh '''rm bin/publish -rf
            dotnet publish -c Release -f net6.0 -o bin/publish
            '''
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
