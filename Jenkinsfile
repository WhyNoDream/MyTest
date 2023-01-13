projectname="gz-net-prod";
gitpath="http://github.com/WhyNoDream/MyTest.git";
workpath="/data/build/wms";///share/wms-jenkins
dllpath="/data/build/lib";//share/wms-jenkins/lib
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

pipeline {
    agent any
    stages {
        stage('拉取代码') {
            steps {
                echo '拉取成功'
            }
        }
        stage('执行构建') {
            steps {
                echo '构建完成'
            }
        }
        stage('把jar包构建为docker镜像并运行') {
            steps {
                echo '运行成功'
            }
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
