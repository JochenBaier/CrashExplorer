#This file is part of CrashExplorer.
#
#CrashExplorer is free software: you can redistribute it and/or modify
#it under the terms of the GNU General Public License as published by
#the Free Software Foundation, either version 3 of the License, or
#(at your option) any later version.
#
#CrashExplorer is distributed in the hope that it will be useful,
#but WITHOUT ANY WARRANTY; without even the implied warranty of
#MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
#GNU General Public License for more details.
#
#You should have received a copy of the GNU General Public License
#along with CrashExplorer.  If not, see <http://www.gnu.org/licenses/>.

add_executable(test_project main.cpp)

target_include_directories(test_project PRIVATE "static_library") 
target_link_libraries( test_project PRIVATE "static_library" )

target_include_directories( test_project PRIVATE "dynamic_library") 
target_link_libraries( test_project PRIVATE "dynamic_library" )

#Generate map file
target_link_options(test_project PRIVATE "/MAP")