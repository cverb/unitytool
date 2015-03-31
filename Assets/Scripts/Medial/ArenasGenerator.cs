﻿using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Medial{
	public class ArenasGenerator  {
		List <List<Vector3>> layers;
		List<List<int>> polygons;
		int option;
		int nlayers;
		public ArenasGenerator(int selGridInt){
			option=selGridInt;
			layers =new List<List<Vector3>>();

			switch(option){
			//for polygon with moving guard
			case 0:polygons= new List<List<int>>{new List<int>{0,1,2,3,4,5,6,7}, new List<int>{10,9,8}};
				nlayers=40;
				generate_arena_with_one_alcove();
				generate_movrot_gaurd();
				break;

			//for convex polygon test
			case 1:polygons= new List<List<int>>{new List<int>{0,1,2,3}};
				nlayers=4;
				generate_convex_arena();
				break;
			case 2: nlayers=20;
				generate_arena();generate_moving_gaurd();
				break;

			case 3: polygons= new List<List<int>>{new List<int>{0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15}, new List<int>{16,17,18}};
				nlayers=40;
				generate_arena_with_three_alcove();
				generate_unidirectional_mov_gaurd();
//				generate_movrot_gaurd();
				break;
			}
		}
		public List <List<Vector3>> getLayers(){
			return layers;
		}
		public List<List<int>> getPolygons(){
			return polygons;
		}

		//to be called on update
		private int layers_i=0;


		public void generate2D_movinggaurdarena(float t){
	//		udl ("layerscount "+layers.Count);
			while(layers_i<layers.Count-1 && t>=layers[layers_i][0].y)
			{
	//			udl ("layer number "+layers_i +" layer.y ="+layers[layers_i][0].y + " t="+t);
				layers_i++;
			}
			if(layers_i>=layers.Count-1)
			{
				layers_i=layers.Count-1;
				t=layers[layers.Count-1][0].y;
			}
			var lowerlayer= layers[layers_i-1];
			var upperlayer= layers[layers_i];
			Vector3 u,v;
			float frac1= upperlayer[0].y-t, frac2= t- lowerlayer[0].y;
			foreach(var poly in polygons){
				for(int i=0;i<poly.Count;i++){
					u=(lowerlayer[poly[i]]*frac1 + upperlayer[poly[i]]*frac2)/(frac1+frac2);
					v=(lowerlayer[poly[(i+1)%poly.Count]]*frac1+ upperlayer[poly[(i+1)%poly.Count]]*frac2)/(frac1+frac2);
					u.y=v.y=20.25f;
					UnityEngine.Debug.DrawLine(u,v,Color.magenta);
				}
			}
	//
	//		int tl=(int) Math.Floor(t), tu= (int)Math.Ceiling(t);
	//		double frac= t-tl;
	//		if(tu>layers.Count)
	//			return;
	//		if(tu>=layers.Count)
	//		{
	//			tl--;tu--;
	//		}
	//
	//		var lowerlayer=layers[tl];
	//		var upperlayer=layers[tu];
	//		Vector3 u,v;
	//		foreach(var poly in polygons){
	//			for(int i=0;i<poly.Count;i++){
	//				u=lowerlayer[poly[i]]*(float)(1-frac) + upperlayer[poly[i]]*(float)(frac);
	//				v=lowerlayer[poly[(i+1)%poly.Count]]*(float)(1-frac)+ upperlayer[poly[(i+1)%poly.Count]]*(float)(frac);
	//				u.y=v.y=20.25f;
	//				UnityEngine.Debug.DrawLine(u,v,Color.magenta);
	//			}
	//		}

		}

		public List<int[]>[] getCovers(){
			Covers c= new Covers(layers,polygons);
			return c.getCovers();
		}


		public static void udl(object s){
			UnityEngine.Debug.Log(s);
		}
		public void generate_convex_arena(){
			Vector3 one= new Vector3(-3f,0f,-2f),
			two=new Vector3(4f,0f,0f), 
			three= new Vector3(-3f,0f,2f), 
			four= new Vector3(-4f,0f,0f);
			for(int ilayer=0;ilayer<nlayers;ilayer++){
				one.y= two.y= three.y= four.y=4f*ilayer;
				layers.Add(new List<Vector3>{one, two,three,four});
			}

		}

		public void generate_arena_with_one_alcove(){
			Vector3 one= new Vector3(-6f,0f,0f),
			two=new Vector3(-2f,0f,0f), 
			three= new Vector3(-2f,0f,-2f), 
			four= new Vector3(2f,0f,-2f),
			five=new Vector3(2f,0f,0f), 
			six=new Vector3(6f,0f,0f),
			sev=new Vector3(6f,0f,4f), 
			ei=new Vector3(-6f,0f,4f); 

			for(int ilayer=0;ilayer<nlayers;ilayer++){
				one.y= two.y= three.y= four.y=five.y=six.y=sev.y=ei.y=1f*ilayer/2;
				layers.Add(new List<Vector3>{one, two,three,four,five,six,sev,ei});
			}

		}

		public void generate_arena_with_three_alcove(){
			Vector3 v01= new Vector3(-6f,0f,0f),
			v02=new Vector3(-4f,0f,0f), 
			v03= new Vector3(-4f,0f,-2f),
			v04= new Vector3(-2f,0f,-2f),
			v05=new Vector3(-2f,0f,0f), 
			v06=new Vector3(2f,0f,0f),
			v07=new Vector3(2f,0f,-2f), 
			v08=new Vector3(4f,0f,-2f),
			v09=new Vector3(4f,0f,0f),
			v10=new Vector3(6f,0f,0f),
			v11=new Vector3(6f,0f,5f),
			v12=new Vector3(0f,0f,5f),
			v13=new Vector3(0f,0f,7f),
			v14=new Vector3(-2f,0f,7f),
			v15=new Vector3(-2f,0f,5f),
			v16=new Vector3(-6f,0f,5f); 
			
			for(int ilayer=0;ilayer<nlayers;ilayer++){
				v01.y=v02.y=v03.y=v04.y=v05.y=v06.y=v07.y=v08.y=v09.y=v10.y=v11.y=v12.y=v13.y=v14.y=v15.y=v16.y=1f*ilayer/2;
				layers.Add(new List<Vector3>{v01,v02,v03,v04,v05,v06,v07,v08,v09,v10,v11,v12,v13,v14,v15,v16});
			}
			
		}
		
		public void generate_arena(){
			Vector3 one= new Vector3(-4f,0f,-4f),
			two=new Vector3(4f,0f,-4f), 
			three= new Vector3(4f,0f,4f), 
			four= new Vector3(-4f,0f,4f);
	//		List<List<Vector3>> arena= new List<List<Vector3>>();
			for(int ilayer=0;ilayer<nlayers;ilayer++){
				one.y= two.y= three.y= four.y=1f*ilayer;
				layers.Add(new List<Vector3>{one, two,three,four});
			}
		}

		public void generate_moving_gaurd(){
	//		udl ("number of nodes= "+layers.Count);
			Vector3 fo=new Vector3(-4f,0,2.5f), so=new Vector3(-4f,0,0.5f), svo= new Vector3(-5.5f,0,1.5f);
			Vector3 ff=new Vector3(5.5f,0,2.5f), sf=new Vector3(5.5f,0,0.5f), svf= new Vector3(4f,0,1.5f);//y don't matter here
			int ilayer;
			Vector3 f= new Vector3(),s=new Vector3(),sv=new Vector3();
			for(ilayer=0;ilayer<nlayers/2f;ilayer++){
				f=(ff*ilayer+fo*(nlayers/2 -ilayer))/(nlayers/2f);
				s=(sf*ilayer+so*(nlayers/2 -ilayer))/(nlayers/2f);
				sv=(svf*ilayer+svo*(nlayers/2 -ilayer))/(nlayers/2f);
				f.y=s.y=sv.y= 1f*ilayer/2f;
				layers[ilayer].Add(f);layers[ilayer].Add(s);layers[ilayer].Add(sv);
			}
			
			fo=s;so=f;svo=sv;
			ff.x=-4f; sf.x=-4f; svf.x=-5.5f;
			float n= nlayers/2;
			for(float i=0f;ilayer<nlayers;ilayer++, i+=1/n){
				f=(sf*i+fo*(1-i));
				s=ff*i+so*(1-i);
				sv=svf*i+svo*(1-i);
				f.y=s.y=sv.y= 1f*ilayer/2f;
				
				layers[ilayer].Add(f);layers[ilayer].Add(s);layers[ilayer].Add(sv);
			}
		}
		
		public void generate_movrot_gaurd(){
	//		udl ("number of nodes= "+layers.Count);
			Vector3 fo=new Vector3(-3f,0,4.5f), so=new Vector3(-3f,0,0.5f), svo= new Vector3(-5.5f,0,1.5f);
			Vector3 ff=new Vector3(5.5f,0,4.5f), sf=new Vector3(5.5f,0,0.5f), svf= new Vector3(3f,0,1.5f);
			int ilayer;
			Vector3 f= new Vector3(),s=new Vector3(),sv=new Vector3();
			for(ilayer=0;ilayer<nlayers/4f;ilayer++){
				f=(ff*ilayer+fo*(nlayers/4 -ilayer))/(nlayers/4);
				s=(sf*ilayer+so*(nlayers/4 -ilayer))/(nlayers/4);
				sv=(svf*ilayer+svo*(nlayers/4 -ilayer))/(nlayers/4);
				f.y=s.y=sv.y= 1f*ilayer/2f;
				layers[ilayer].Add(f);layers[ilayer].Add(s);layers[ilayer].Add(sv);
			}
			
			var theta = 180f/(nlayers/2f);
			for(;ilayer<3f*nlayers/4f;ilayer++){
				f=RotatePointAroundPivot(f,sv,new Vector3(0, -theta, 0));
				s=RotatePointAroundPivot(s,sv,new Vector3(0, -theta, 0));
				f.y=s.y=sv.y=1f*ilayer/2f;
				
				layers[ilayer].Add(f);layers[ilayer].Add(s);layers[ilayer].Add(sv);
			}
			
			fo=f;so=s;svo=sv;
			ff.x=-5.5f; sf.x=-5.5f; svf.x=-3f;
			float n= nlayers/4;
			for(float i=0f;ilayer<nlayers;ilayer++, i+=1/n){
				f=(sf*i+fo*(1-i));
				s=ff*i+so*(1-i);
				sv=svf*i+svo*(1-i);
				f.y=s.y=sv.y= 1f*ilayer/2f;
				
				layers[ilayer].Add(f);layers[ilayer].Add(s);layers[ilayer].Add(sv);
			}
	//		udl ("number of nodes final= "+layers.Count);
	//		return layers;
		}
		public void generate_unidirectional_mov_gaurd(){
			//		udl ("number of nodes= "+layers.Count);
			Vector3 fo=new Vector3(-3f,0,4f), so=new Vector3(-3f,0,1f), svo= new Vector3(-5.5f,0,2.5f);
			Vector3 ff=new Vector3(5.5f,0,4f), sf=new Vector3(5.5f,0,1f), svf= new Vector3(3f,0,2.5f);
			int ilayer;
			Vector3 f= new Vector3(),s=new Vector3(),sv=new Vector3();
			for(ilayer=0;ilayer<nlayers;ilayer++){
				f=(ff*ilayer+fo*(nlayers -ilayer))/(nlayers);
				s=(sf*ilayer+so*(nlayers -ilayer))/(nlayers);
				sv=(svf*ilayer+svo*(nlayers -ilayer))/(nlayers);
				f.y=s.y=sv.y= 1f*ilayer/2f;
				layers[ilayer].Add(f);layers[ilayer].Add(s);layers[ilayer].Add(sv);
			}
		}
		public void generate_rotating_gaurd(){
			Vector3 f=new Vector3(-2f,0,-1f), s=new Vector3(-2f,0,3f), sv= new Vector3(2f,0,1f);
			for(int ilayer=0;ilayer<nlayers;ilayer++){
				f=RotatePointAroundPivot(f,sv,new Vector3(0, -18f, 0));
				s=RotatePointAroundPivot(s,sv,new Vector3(0, -18f, 0));
				//sv=q * sv;
				f.y=s.y=sv.y=1f*ilayer;
				
				layers[ilayer].Add(f);layers[ilayer].Add(s);layers[ilayer].Add(sv);
			}
	//		return layers;
		}

		
		
		Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles) {
			Vector3 dir = point - pivot; // get point direction relative to pivot
			dir = Quaternion.Euler(angles) * dir; // rotate it
			point = dir + pivot; // calculate rotated point
			return point; // return it
		}
		//	void TriangulateFirstandLastLayer(List<List<Vector3>> layers, List<List<int>> polygons){
		//		List<Ligne> allines,actuallines= new List<Ligne>();
		//		List<Vector3>allvertices=new List<Vector3>();
		//		allvertices.AddRange(layers[0]);
		//		bool [,]LineMap= new bool[allvertices.Count,allvertices.Count];
		//		int ii=0;
		//		foreach(var polygon in polygons){
		//			for(int i=0; i<polygon.Count; i++ ){
		//				actuallines.Add(new Ligne(allvertices[i+ii],allvertices[((i+1)%polygon.Count)+ii
		//				                                                    ,i+ii,((i+1)%polygon.Count)+ii]));
		//				LineMap[i+ii,((i+1)%polygon.Count)+ii]=true;
		//			}
		//			ii+=polygon.Count;
		//		}
		//
		//		allines= new List<Ligne>(actuallines);
		//
		//		foreach(var line in actuallines){
		//			var v=line.vertexIndex;
		//			Vector3 v3=alreadyConnectedNotAdjacentVertex(v[0],v[1],LineMap);
		//			SortedList sl= ClosestVertex(allvertices[v[0]],allvertices[v[1]],allvertices);
		//			int k=0;
		//			//get the next closest vertex
		//			while(k<allvertices.Count-2){
		//				v3=sl[k];
		//				Ligne l1= new Ligne(v3,v[0]),l2= new Ligne(v3,v[1]);
		//
		//				//check if v3-v1 and v3-v2 doesn't intersect other lines
		//				foreach(var otherline in allines){
		//					//ignore lines passing through the 3rd vertex
		//					if(otherline.vertex[0]== v3||otherline.vertex[1]== v3)
		//						continue;
		//					if(l1.LineIntersection(otherline)||l2.LineIntersection(otherline)){
		//						break;
		//					}
		//				}
		//			}
		//		}
		//	}
		//
		//
		//	bool notIntersectAnyLine(){}
		
		//
		//	Vector3 alreadyConnectedNotAdjacentVertex(int a, int b, bool[,] LineMap){
		//		for(int i=0;i<LineMap.GetLength(1);i++){
		//			if(i!=a && i!=b && (LineMap[i][a] || LineMap[a][i]) && (LineMap[i][b] || LineMap[b][i])){
		//				return true;
		//			}
		//		}
		//		return false;
		//	}
		//	SortedList ClosestVertex(Vector3 v1,Vector3 v2, List<Vector3> list){
		//		var m= (v1 +v2)/2f;SortedList sl= new SortedList();
		//		for(int i=0;i<list.Count;i++){
		//			var v3=list[i];
		//			if(!v3.Equals(v1) && !v3.Equals(v2)){
		//				sl.Add(Vector3.Distance(m,v3),i);
		//			}
		//		}
		//		return sl;
		//	}

		/*void readcsv(){
			var reader = new StreamReader(File.OpenRead(@"C:\test.csv"));
			List<string> listA = new List<string>();
			List<string> listB = new List<string>();
			while (!reader.EndOfStream)
			{
				var line = reader.ReadLine();
				var values = line.Split(';');

				listA.Add(values[0]);
				listB.Add(values[1]);
			}

		}*/

	}
	class Triangle{
		int a,b,c;
		Vector3 av,bv,cv;
	}
}