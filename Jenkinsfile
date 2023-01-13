projectname="gz-net-prod";
gitpath="http://github.com/WhyNoDream/MyTest.git";
workpath="/data/build/wms";///share/wms-jenkins
dllpath="/data/build/lib";//share/wms-jenkins/lib
version="v3.1";

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
