#!/usr/bin/env sh
commitMessage="[Dipesh] modify GenderSumAdded"
currentBranchName="UC6GenderSum"
newBranchName="UC7InsertEmployee"
echo --------git status---------
echo
git status
echo ---------git add *--------------
echo
git add *
echo ------------git commit -m $commitMessage-----------------
echo
git commit -m "$commitMessage"
echo ------------git push-------------------
echo
git remote show origin
git push -u origin $currentBranchName
echo ------------git checkout Master-------------------
echo
git checkout master
echo ------------git pull-------------------
echo
git merge $currentBranchName
echo ------------git branch and checkout-------------------
echo
git branch $newBranchName
git checkout $newBranchName